using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayerIntegration_ETL
{
    class ProcessPR
    {
        public static void ProcessPriorRequest()
        {

            try
            {


                Helpers.Logger.Info("-- Processing PRior Request -- ");
                string[] fileName = Helpers.FileHelper.getFiles();

                string ParallelProcessing = System.Configuration.ConfigurationManager.AppSettings["ParellelProcessing"];

                if (bool.Parse(ParallelProcessing))
                {
                    Parallel.ForEach(fileName, (file) =>
                  {

                      Helpers.Logger.Info(System.IO.Path.GetFileName(file));

                      //Helpers.XMLHelper.split_xml(file);
                      Helpers.XMLHelper.XmlTextReader(file);
                      //     Helpers.FileHelper.MoveFiles(file);
                  }
                        );
                }
                else
                {
                    foreach (var file in fileName)
                    {
                        Helpers.Logger.Info(System.IO.Path.GetFileName(file));

                        //Helpers.XMLHelper.split_xml(file);
                        Helpers.XMLHelper.XmlTextReader(file);


                        //     Helpers.FileHelper.MoveFiles(file);
                    }
                }
            }
            catch (Exception ex)
            {
                Helpers.Logger.Error(ex);
            }

        }

        public static void ProcessPriorRequestDB()
        {
            try
            {
                Helpers.Logger.Info("-- Processing PRior Request -- ");
              
                string ParallelProcessing = System.Configuration.ConfigurationManager.AppSettings["ParellelProcessing"];

                if (bool.Parse(ParallelProcessing))
                {

                    Helpers.DBHelper DBobj = new Helpers.DBHelper("Server=10.11.13.43;Database=PBMM; user ID=fshaikh; Password=Dell@200;");//providerName = "System.Data.SqlClient");
                    
                    System.Data.DataSet DSAllID =  DBobj.ExecDataSet(Helpers.DBQueries.PriorRequestIDs());

                    List<string> IDList = new List<string>();
                    foreach (System.Data.DataRow dr in DSAllID.Tables[0].Rows)
                    {
                        IDList.Add(dr[0].ToString());

                    }
                    foreach (var item in IDList)
                    {
                        Helpers.Logger.Info("" + item);
                        System.Data.DataSet DSTransData = DBobj.ExecDataSet(Helpers.DBQueries.PriorRequestTransactionbyID(item));

                        Helpers.XMLHelper.DataSettoXmlTextWriter(DSTransData);

                        foreach (System.Data.DataRow dr in DSTransData.Tables[0].Rows)
                        {
                            // IDList.Add(dr[0].ToString());
                            Helpers.Logger.Info("" + dr[1].ToString());
                        }
                    }

                    //                  



                }
            }
            catch (Exception ex)
            {

                Helpers.Logger.Error(ex);


                throw;
            }
        }
    }
}