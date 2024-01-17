using OpenCvSharp;

class SaveFrames
{

    public static void saveFrame(string pathFile, string pathSaveFile)
    {
        // Crear el objeto de captura de video
        using (var capture = new VideoCapture(pathFile))
        {
            int index = 0;
            Mat frame = new Mat();

            while (true)
            {
                // Leer el siguiente frame del video
                capture.Read(frame);

                // Si el frame es null, entonces hemos llegado al final del video
                if (frame.Empty())
                    break;

                // Guardar el frame como una imagen
                frame.SaveImage($"{pathSaveFile}//frame{index}.png");
                index++;
            }
            Console.WriteLine("Frames saved successfully");

        }
    }
}