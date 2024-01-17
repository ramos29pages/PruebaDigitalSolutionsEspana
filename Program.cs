using System;
using System.IO;
using OpenCvSharp;
using OpenCvSharp.Face;

class Program
{

    static void Main(string[] args)
    {

        /*
        ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        ******************* ¡¡¡ IMPORTANTE !!! ******************
        +++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        */
        string pathRoot = Directory.GetCurrentDirectory();

        // Ruta del video de una persona
        string videoPath = @$"{pathRoot}\public\videoprueba.mp4";

        //RUTA PARA GUARDAR LAS IMAGENES (FRAMES) DEL VIDEO
        string pathSaveFile = @$"{pathRoot}\public\FRAMES";

        //RUTA PARA GUARDAR LAS IMAGENES CON LAS DIFERENCIAS FACIALES DEL VIDEO
        string pathFeatures =  @$"{pathRoot}\public\FEATURE-FACES";

        // Ruta del modelo de detección de caras
        string faceModel = @$"{pathRoot}\models\haarcascade_frontalface_default.xml";

        // Ruta del modelo de estimación de puntos de referencia faciales
        string landmarkModel = @$"{pathRoot}\models\lbfmodel.yaml";


        /*
        ++++++++++++++++++++++++++++++++++++++
        GUARDAMOS TODOS LOS FRAMES DEL VIDEO
        ++++++++++++++++++++++++++++++++++++++
        */
        SaveFrames.saveFrame(videoPath, pathSaveFile);



        /*
        ++++++++++++++++++++++++++++++++++++++++++++++++++
        GUARDAMOS TODOS LAS DIFERENCIAS FACIALES DEL VIDEO
        ++++++++++++++++++++++++++++++++++++++++++++++++++
        */
        using (var video = new VideoCapture(videoPath))
        {
            // Crear un objeto FaceAnalyzer para analizar las caras y las diferencias faciales
            var faceAnalyzer = new FaceAnalyzer(faceModel, landmarkModel, pathFeatures);

            // Crear un bucle para iterar sobre los frames del video
            while (true)
            {
                // Leer el frame actual del video
                using (var frame = video.RetrieveMat())

                    // Si el frame es nulo, significa que se ha llegado al final del video
                    if (frame.Empty())
                    {
                        break;
                    }
                    else
                    {
                        // Analizar las caras y las diferencias faciales del frame
                        Console.Write(frame.ToString() + " -> ");
                        faceAnalyzer.analyze(frame);

                    }

            }

            Console.WriteLine("**********************************.");
            Console.WriteLine("Faces add successfully.");
            Console.WriteLine("**********************************.");

        }
    }
}





