namespace WebGallery.Utils {
    public class DotEnv {
        public static void Config(string filePath) {
            if (!File.Exists(filePath)) throw new IOException("File does not exist");

            try {
                using (var sr = new StreamReader(filePath)) {
                    while (!sr.EndOfStream) {
                        string[] line = sr.ReadLine().Split('=');
                        if (line.Length != 2) continue;

                        Environment.SetEnvironmentVariable(line[0], line[1]);

                    }
                }

            } catch (IOException ex) {
                Console.WriteLine("Error while reading file: " + ex.StackTrace);
            }
        }

        public static void Config() {
            var Root = Directory.GetCurrentDirectory();
            var DotEnv = Path.Combine(Root, ".env");

            Config(DotEnv);
        }
    }
}
