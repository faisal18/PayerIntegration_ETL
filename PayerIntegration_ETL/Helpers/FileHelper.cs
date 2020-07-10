using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;


namespace PayerIntegration_ETL.Helpers
{
    class FileHelper
    {

        public static string[] getFiles()
        {
            try
            {
                string FileIDsPath = ConfigurationManager.AppSettings["SourceXMLFilePath"];
                string[] FileIDs = Directory.GetFiles(FileIDsPath, "*.xml");
                return FileIDs;
            }
            catch (Exception ex)
            {

                Logger.Error(ex);
                return null;
            }

        }


        public static bool SaveAnyFile(string filename, string FileContent, string Fullpath, bool append, string fileextension)
        {

            try
            {
                string path = Fullpath + filename;
                bool FileResults = false;



                if (FileContent.Length > 0 && filename.Length > 0 && Fullpath.Length > 0 && fileextension.Length > 0)
                {



                    if (!System.IO.File.Exists(path))
                    {
                        using (System.IO.StreamWriter _testData = new System.IO.StreamWriter(path, append))
                        {
                            _testData.WriteLine(FileContent); // Write the file.
                            _testData.Close();
                            _testData.Dispose();
                        }
                        Logger.Info(path.ToString() + "-- Saved");

                        FileResults = true;
                    }

                }
                else
                {
                    FileResults = false;
                }


                return FileResults;

            }
            catch (Exception)
            {

                return false;

            }
        }



        public static bool MoveFiles(string filename)
        {
            try
            {
                string ProcessedFIlePath = ConfigurationManager.AppSettings["ProcessedXMLFilePath"];
                if (File.Exists(filename))
                {
                    File.Move(filename, ProcessedFIlePath + "\\" + Path.GetFileName(filename));
                    Logger.Info("   file moved !!!");
                }
                else
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {

                Logger.Error(ex);
                return false;
            }

        }
    }
}
