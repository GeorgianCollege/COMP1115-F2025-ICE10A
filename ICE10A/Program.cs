using System.Diagnostics;

namespace ICE10A
{
    public enum FormType
    {
        Splash,
        Start,
        Next,
        Final,
        About
    }

    public enum ToastType
    {
        Success,
        Danger,
        Warning,
        Info
    }

    internal static class Program
    {
        // Declaring Form Variables
        public static SplashForm SplashForm;
        public static SelectionForm SelectionForm;
        public static NextForm NextForm;
        public static FinalForm FinalForm;
        public static AboutForm AboutForm;

        // Declaring a List of Type Form
        public static List<Form> Forms;

        // Declaring a bool that will help us exit
        public static bool IsExiting = false;


        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Initializing the Form Variables
            // by instantiating objects of the related form types
            SplashForm = new SplashForm();
            SelectionForm = new SelectionForm();
            NextForm = new NextForm();
            FinalForm = new FinalForm();
            AboutForm = new AboutForm();

            // Initializing the Forms List
            Forms =
            [
                SplashForm,
                SelectionForm,
                NextForm,
                FinalForm,
                AboutForm
            ];

            Application.Run(SplashForm);
        }

        public static void ConfirmExit(FormClosingEventArgs e)
        {
            if (IsExiting)
            {
                return;
            }

            var result = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                IsExiting = true;
                Application.Exit();
            }
        }

        public static void SaveCharacter(string path)
        {
            using StreamWriter writer = new StreamWriter(path);
            writer.WriteLine(Settings.Default.AGL);
            writer.WriteLine(Settings.Default.STR);
            writer.WriteLine(Settings.Default.VGR);
            writer.WriteLine(Settings.Default.PER);
            writer.WriteLine(Settings.Default.INT);
            writer.WriteLine(Settings.Default.WIL);
            writer.WriteLine(Settings.Default.CharacterName);
            writer.WriteLine(Settings.Default.Species);
            writer.WriteLine(Settings.Default.Career);
        }

        public static void LoadCharacter(string path)
        {
            try
            {
                // Check for file existence
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException("Character File does not exist");
                }

                // Check for empty file
                FileInfo info = new FileInfo(path);
                if (info.Length == 0)
                {
                    throw new FileFormatException("Character file is empty");
                }

                // Quick check for file validity by counting the number of lines
                string[] lines = File.ReadAllLines(path);
                if (lines.Length < Settings.Default.Properties.Count)
                {
                    throw new FileFormatException("Invalid Character file");
                }

                using StreamReader reader = new StreamReader(path);

                var AGL = reader.ReadLine();
                var STR = reader.ReadLine();
                var VGR = reader.ReadLine();
                var PER = reader.ReadLine();
                var INT = reader.ReadLine();
                var WIL = reader.ReadLine();
                var CharacterName = reader.ReadLine();
                var Species = reader.ReadLine();
                var Career = reader.ReadLine();

                if (AGL == null || STR == null || VGR == null || PER == null || INT == null || WIL == null ||
                    CharacterName == null || Species == null || Career == null)
                {
                    throw new FileFormatException("Invalid Character file");
                }
                Settings.Default.AGL = AGL;
                Settings.Default.STR = STR;
                Settings.Default.VGR = VGR;
                Settings.Default.PER = PER;
                Settings.Default.INT = INT;
                Settings.Default.WIL = WIL;
                Settings.Default.CharacterName = CharacterName;
                Settings.Default.Species = Species;
                Settings.Default.Career = Career;

            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show("File Not Found: " + e.Message, "File Not Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FileFormatException e)
            {
                MessageBox.Show("Format Error: " + e.Message, "Format Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}