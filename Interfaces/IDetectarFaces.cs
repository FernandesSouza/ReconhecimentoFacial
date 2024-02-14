namespace DetectarFace.Interfaces
{
    public interface IDetectarFaces
    {
        Task<string> DetectarFace(IFormFile file);

        Task<double> CalculateEuclideanDistance(double[] features1, double[] features2);

        Task<bool> ReconhecimentoFacial();

    }
}
