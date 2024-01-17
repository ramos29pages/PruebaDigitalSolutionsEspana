using OpenCvSharp;
using System.Collections.Generic;
using System;
using System.IO;
using OpenCvSharp.Face;

class FaceAnalyzer
{
    // Atributos de la clase
    private CascadeClassifier faceDetector;
    private FacemarkLBF facemark;
    private string faceModel;
    private string landmarkModel;
    private string facesFolder;

    public FaceAnalyzer(string faceModel, string landmarkModel, string facesFolder)
    {
        this.faceModel = faceModel;
        this.landmarkModel = landmarkModel;
        this.facesFolder = facesFolder;

        // Crear e inicializar los objetos faceDetector y facemark
        faceDetector = new CascadeClassifier(faceModel);
        facemark = FacemarkLBF.Create();
        facemark.LoadModel(landmarkModel);
    }

    // Método para analizar las caras y las diferencias faciales de un frame
    public void analyze(Mat frame)
    {

        // Detectar las caras en el frame
        Rect[] faces = faceDetector.DetectMultiScale(frame);

        // Verificar si se detectaron caras
        if (faces.Length > 0)
        {
            Console.WriteLine("Caras encontradas ::: "+faces.Length );
            // Crear una lista para almacenar los puntos de referencia faciales de cada cara
            var landmarks = new Point2f[faces.Length][];

            // Crear un InputArray a partir del arreglo de Rect
            using (InputArray iaFaces = InputArray.Create(faces))
            {
                // Estimar los puntos de referencia faciales de cada cara
                facemark.Fit(frame, iaFaces, out landmarks);
            }


            // Crear una imagen vacía del mismo tamaño que el frame
            using (var faceImage = new Mat(frame.Size(), MatType.CV_8UC3))

            // Dibujar las caras y los puntos de referencia faciales en la imagen vacía
            {
                // Recorrer cada cara detectada
                for (int i = 0; i < faces.Length; i++)
                {
                    // Dibujar un rectángulo alrededor de la cara con un color aleatorio
                    Cv2.Rectangle(faceImage, faces[i], Scalar.RandomColor(), 2);

                    // Recorrer cada punto de referencia facial de la cara
                    foreach (var point in landmarks[i])
                    {
                        // Dibujar un círculo en el punto con un color aleatorio
                        Cv2.Circle(faceImage, (Point)point, 2, Scalar.RandomColor(), -1);
                    }
                }

                // Crear el nombre del archivo para guardar la imagen con las diferencias faciales
                string faceName = $"face{DateTime.Now.Ticks}.jpg";

                // Crear la ruta completa del archivo
                string facePath = Path.Combine(facesFolder, faceName);

                // Guardar la imagen con las diferencias faciales en la carpeta especificada
                faceImage.SaveImage(facePath);
            }
        }
        else
        {
            Console.WriteLine("No se detectaron caras en el frame.");
        }

    }
}