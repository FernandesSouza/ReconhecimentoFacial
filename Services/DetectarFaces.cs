using Emgu.CV.Structure;
using Emgu.CV;
using System.Drawing;
using Emgu.CV.CvEnum;
using DetectarFace.Interfaces;

namespace DetectarFace.Services
{
    public class DetectarFaces : IDetectarFaces
    {
        private readonly CascadeClassifier haarCascade;
        public DetectarFaces()
        {
            haarCascade = new CascadeClassifier("haarcascade_frontalface_default.xml");
        }
        public Task<double> CalculateEuclideanDistance(double[] features1, double[] features2)
        {
            if (features1.Length != features2.Length)
                throw new ArgumentException("Os vetores de características devem ter o mesmo tamanho");

            double sum = 0.0;
            for (int i = 0; i < features1.Length; i++)
            {
                sum += Math.Pow(features1[i] - features2[i], 2);
            }
            return Task.FromResult(Math.Sqrt(sum));
        }

        public async Task<string> DetectarFace(IFormFile file)
        {
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                var bytes = stream.ToArray();
                var localFileName = "C:\\Users\\Igor\\Desktop\\CsharpEstudos\\FaceDetect\\DetectarFace\\DetectarFace\\Images\\inputFace.jpg";
                if (File.Exists(localFileName) is false)
                    File.Create(localFileName).Dispose();
                await File.WriteAllBytesAsync(localFileName, bytes);
                Image<Bgr, Byte> grayFrame = new Image<Bgr, byte>(localFileName);
                Rectangle[] faces = haarCascade.DetectMultiScale(grayFrame, 1.2, 10);

                if (faces.Count() == 0)
                    return string.Empty;

                foreach (var face in faces)
                {
                    grayFrame.Draw(face, new Bgr(255, 255, 0), 1);
                }

                var result = grayFrame.ToJpegData();
                var resultFileName = "C:\\Users\\Igor\\Desktop\\CsharpEstudos\\FaceDetect\\DetectarFace\\DetectarFace\\Images\\faceResult.jpg";

                if (File.Exists(resultFileName) is false)
                    File.Create(resultFileName).Dispose();

                await File.WriteAllBytesAsync(resultFileName, result);

                return resultFileName;
            }
        }

        public async Task<bool> ReconhecimentoFacial()
        {
            Mat imageResult = CvInvoke.Imread("C:\\Users\\Igor\\Desktop\\CsharpEstudos\\FaceDetect\\DetectarFace\\DetectarFace\\Images\\faceResult2.jpg", ImreadModes.Grayscale);

            Mat referenceImage = CvInvoke.Imread("C:\\Users\\Igor\\Desktop\\CsharpEstudos\\FaceDetect\\DetectarFace\\DetectarFace\\Images\\faceResult1.jpg", ImreadModes.Grayscale);

            Rectangle[] referenceFaces = haarCascade.DetectMultiScale(referenceImage, 1.2, 10);
            Rectangle[] detectedFaces = haarCascade.DetectMultiScale(imageResult, 1.2, 10);

            if (referenceFaces.Length == 0 || detectedFaces.Length == 0)
                return false;

            double[] featuresReference = { referenceFaces[0].X, referenceFaces[0].Y, referenceFaces[0].Width, referenceFaces[0].Height };
            double[] featuresDetected = { detectedFaces[0].X, detectedFaces[0].Y, detectedFaces[0].Width, detectedFaces[0].Height };


            double distance = await CalculateEuclideanDistance(featuresReference, featuresDetected);

            double valorMin = 100.0;

            bool Valido = distance < valorMin;

            return Valido;

        }
    }
}
