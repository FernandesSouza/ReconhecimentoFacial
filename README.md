# API de Reconhecimento Facial com a Biblioteca Emgu

Este projeto implementa uma API de reconhecimento facial usando a biblioteca Emgu. A seguir, estão os detalhes dos endpoints disponíveis na API:

Por favor, note que o processo não é totalmente automatizado. Portanto, algumas interações manuais podem ser necessárias.

## POST --> /Api/Detectar/detectar 

Este endpoint simula o processo de login do usuário através do FaceId. Quando acionado, ele abre a câmera para capturar a imagem do usuário. A imagem é então analisada e a face é detectada. A área ao redor da face detectada é destacada com um quadrado.

## GET --> /Api/Detectar/{fileName}

Este endpoint é usado principalmente para fins de teste. Ele permite verificar se o método POST funcionou corretamente, retornando a imagem capturada com a face do usuário destacada por um quadrado.

## GET --> /Api/Detectar/

Este endpoint compara as imagens resultantes do método POST usando a fórmula de distância Euclidiana. Se a distância mínima obtida for maior que 100.00, o endpoint retorna FALSE. Caso contrário, retorna TRUE.


