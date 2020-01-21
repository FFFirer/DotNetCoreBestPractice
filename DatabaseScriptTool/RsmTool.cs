using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace DatabaseScriptTool
{
    public class RsmTool
    {
        static string connectstring = "Server=172.16.98.213;Database=RSM1009;Uid=sa;Pwd=Sa123456";

        public static void AddStationEng(int TotalCount, int CountPerPage)
        {
            // data seed
            Random r_userId = new Random(1);
            Random r_stationtype = new Random(0);
            Random r_scheduledate_hour = new Random(1);
            Random r_group_id = new Random(1);
            // sql
            string sql = "INSERT INTO [dbo].[T_StationEng] ([ScheduleDate] ,[T_User_ID] ,[StationType],[UpdateUserID] ,[Description] ,[T_Group_ID])" +
                " VALUES (@ScheduleDate, @T_User_ID, @StationType, @UpdateUserID, @Description, @T_Group_ID)";
            
            using(SqlConnection connection = new SqlConnection(connectstring))
            {
                int CountDone = 0;
                List<T_StationEng> data = null;
                
                T_StationEng stationEng = null;
                DataTable tempDt = new DataTable();
                tempDt.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("ID", typeof(int)),
                    new DataColumn("ScheduleDate", typeof(DateTime)),
                    new DataColumn("T_User_ID", typeof(int)),
                    new DataColumn("StationType", typeof(int)),
                    new DataColumn("UpdateUserID", typeof(int)),
                    new DataColumn("UpdateTime", typeof(DateTime)),
                    new DataColumn("Description", typeof(string)),
                    new DataColumn("OnlineEdit", typeof(int)),
                    new DataColumn("T_Group_ID", typeof(int)),
                    new DataColumn("TimeSlotRatioFirst", typeof(int)),
                    new DataColumn("TimeSlotRatioSecond", typeof(int))
                });
                while (CountDone < TotalCount)
                {
                    //data = new List<T_StationEng>();
                    //for(int i = 0; i < CountPerPage; i++)
                    //{
                    //    // 一批数据
                    //    data.Add(new T_StationEng()
                    //    {
                    //        T_User_ID = r_userId.Next(201),
                    //        ScheduleDate = DateTime.Now.AddDays(-r_scheduledate_hour.Next(100)),
                    //        StationType = r_stationtype.Next(4),
                    //        Description = "test",
                    //        T_Group_ID = r_group_id.Next(19)
                    //    });
                    //}
                    tempDt.Rows.Clear();
                    for (int i = 0; i < CountPerPage; i++)
                    {
                        stationEng = new T_StationEng()
                        {
                            T_User_ID = r_userId.Next(1, 201),
                            ScheduleDate = DateTime.Now.AddDays(-r_scheduledate_hour.Next(100)),
                            StationType = r_stationtype.Next(4),
                            Description = "test1",
                            T_Group_ID = r_group_id.Next(19)

                        };

                        DataRow row = tempDt.NewRow();
                        row[1] = stationEng.ScheduleDate;
                        row[2] = stationEng.T_User_ID;
                        row[3] = stationEng.StationType;
                        row[4] = stationEng.UpdateUserID;
                        row[5] = stationEng.UpdateTime;
                        row[6] = stationEng.Description;
                        row[7] = stationEng.OnlineEdit;
                        row[8] = stationEng.T_Group_ID;
                        row[9] = stationEng.TimeSlotRatioFirst;
                        row[10] = stationEng.TimeSlotRatioSecond;

                        tempDt.Rows.Add(row);
                        CountDone++;
                    }

                    using (SqlBulkCopy bulkcopy = new SqlBulkCopy(connection))
                    {

                        try
                        {
                            connection.Open();
                            bulkcopy.DestinationTableName = "T_StationEng";
                            bulkcopy.BatchSize = tempDt.Rows.Count;
                            bulkcopy.WriteToServer(tempDt);
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }
                        finally
                        {
                            connection.Close();
                            bulkcopy?.Close();
                        }
                    }

                    

                    //connection.Execute(sql, stationEng);
                    //CountDone++;
                    Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} {CountDone} / {TotalCount}");
                }
            }
        }
    }

    #region Domain Models

    /// <summary>
    /// 工程师岗位
    /// </summary>
    public class T_StationEng
    {
        public int ID { get; set; }
        public DateTime ScheduleDate { get; set; }
        public int T_User_ID { get; set; }
        public int StationType { get; set; }
        public int UpdateUserID { get; set; } = 201;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public int OnlineEdit { get; set; } = 0;
        public int T_Group_ID { get; set; } = 1;
        public int TimeSlotRatioFirst { get; set; } = 1;
        public int TimeSlotRatioSecond { get; set; } = 1;
    }
    #endregion
}
