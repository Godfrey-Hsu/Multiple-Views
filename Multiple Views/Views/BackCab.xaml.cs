using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics; //计算工时
using System.IO;
using System.Media;  //播放声音
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using HalconDotNet;



namespace Multiple_Views.Views
{

    /// <summary>
    /// BackCab.xaml 的交互逻辑
    /// </summary>
    public partial class BackCab : UserControl
    {
        List<Label> itemList = new List<Label>(10);   //存放检查项目 界面
        List<TextBox> specList = new List<TextBox>(10);   //存放检查规格 界面
        List<Label> resultList = new List<Label>(10);     //存放检查结果 界面
        List<Label> judgeList = new List<Label>(10);     //存放判断 界面

        private DataTable spectable = new DataTable();
        private Hashtable spechash = new Hashtable();

        int okCount = 0, ngCount = 0;
        public BackCab()
        {
            InitializeComponent();
        }
        //规格读出
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(usercontrol1);  //获取焦点
            Save.IsEnabled = false;

            //检查项目以及规格
            #region 控件list
            itemList.Add(itmlbl0);
            itemList.Add(itmlbl1);
            itemList.Add(itmlbl2);
            itemList.Add(itmlbl3);
            itemList.Add(itmlbl4);
            itemList.Add(itmlbl5);
            itemList.Add(itmlbl6);
            itemList.Add(itmlbl7);
            itemList.Add(itmlbl8);
            itemList.Add(itmlbl9);
            itemList.Add(itmlbl20);
            itemList.Add(itmlbl21);
            itemList.Add(itmlbl22);
            itemList.Add(itmlbl23);
            itemList.Add(itmlbl24);
            itemList.Add(itmlbl25);
            itemList.Add(itmlbl26);
            itemList.Add(itmlbl27);
            itemList.Add(itmlbl28);
            itemList.Add(itmlbl29);

            specList.Add(spctxt0);
            specList.Add(spctxt1);
            specList.Add(spctxt2);
            specList.Add(spctxt3);
            specList.Add(spctxt4);
            specList.Add(spctxt5);
            specList.Add(spctxt6);
            specList.Add(spctxt7);
            specList.Add(spctxt8);
            specList.Add(spctxt9);
            specList.Add(spctxt20);
            specList.Add(spctxt21);
            specList.Add(spctxt22);
            specList.Add(spctxt23);
            specList.Add(spctxt24);
            specList.Add(spctxt25);
            specList.Add(spctxt26);
            specList.Add(spctxt27);
            specList.Add(spctxt28);
            specList.Add(spctxt29);

            resultList.Add(reslbl0);
            resultList.Add(reslbl1);
            resultList.Add(reslbl2);
            resultList.Add(reslbl3);
            resultList.Add(reslbl4);
            resultList.Add(reslbl5);
            resultList.Add(reslbl6);
            resultList.Add(reslbl7);
            resultList.Add(reslbl8);
            resultList.Add(reslbl9);
            resultList.Add(reslbl20);
            resultList.Add(reslbl21);
            resultList.Add(reslbl22);
            resultList.Add(reslbl23);
            resultList.Add(reslbl24);
            resultList.Add(reslbl25);
            resultList.Add(reslbl26);
            resultList.Add(reslbl27);
            resultList.Add(reslbl28);
            resultList.Add(reslbl29);

            judgeList.Add(jdglbl0);
            judgeList.Add(jdglbl1);
            judgeList.Add(jdglbl2);
            judgeList.Add(jdglbl3);
            judgeList.Add(jdglbl4);
            judgeList.Add(jdglbl5);
            judgeList.Add(jdglbl6);
            judgeList.Add(jdglbl7);
            judgeList.Add(jdglbl8);
            judgeList.Add(jdglbl9);
            judgeList.Add(jdglbl20);
            judgeList.Add(jdglbl21);
            judgeList.Add(jdglbl22);
            judgeList.Add(jdglbl23);
            judgeList.Add(jdglbl24);
            judgeList.Add(jdglbl25);
            judgeList.Add(jdglbl26);
            judgeList.Add(jdglbl27);
            judgeList.Add(jdglbl28);
            judgeList.Add(jdglbl29);
            #endregion

            try
            {

                spectable = CsvReader(AppDomain.CurrentDomain.BaseDirectory, "//BackCab.csv"); //不包含文件头
                for (int i = 0; i < spectable.Rows.Count; i++)
                {
                    itemList[i].Content = spectable.Rows[i][0];
                    specList[i].Text = spectable.Rows[i][1].ToString();
                }
            }
            catch (Exception Err)
            {
                MessageBox.Show(Err.Message +","+ spectable.Rows.Count);
                LogSet(Err.Message);
            }
            finally
            {
                GC.Collect();
            }

        }


        /// <summary>
        /// 暂定------>画像检查以及规格判断Trigger
        /// </summary>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            RunCheck();
        }

        //key trigger        
        private void ExeCheck(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.L)
                RunCheck();
        }


        void RunCheck()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start(); //计时开始
            string strLog = null;
            //判断用变量
            try
            {
                int judgeFlag = 1;

                HDevelopExport HD = new HDevelopExport();
                HD.RunHalcon(HWCWPF.HalconWindow);

                //HD.cs_Quality[]  结果数组
                
                for (int i = 0; i < HD.cs_Quality.Length; i++)
                {
                    strLog += HD.cs_Quality[i].F.ToString("f2")+","; //log输出用
                    resultList[i].Content = HD.cs_Quality[i].F.ToString("f2"); //界面显示
                    if (HD.cs_Quality[i].F > Convert.ToDouble(spectable.Rows[i][1]))
                    {
                        this.judgeList[i].Content = "OK";
                        this.judgeList[i].Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                    }
                    else
                    {
                        judgeList[i].Content = "NG";
                        this.judgeList[i].Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                        HOperatorSet.SetColor(HWCWPF.HalconWindow, "red");
                        HOperatorSet.SetDraw(HWCWPF.HalconWindow, "margin");
                        HOperatorSet.SetLineWidth(HWCWPF.HalconWindow, 3);
                        HOperatorSet.DispCircle(HWCWPF.HalconWindow, HD.cs_y[i], HD.cs_x[i], 20);
                        
                        judgeFlag = -1;
                    }

                }


                //总的判断
                if (judgeFlag == 1)
                {
                    this.finalLbl.Content = "OK";
                    this.finalLbl.Foreground = new SolidColorBrush(Colors.Green);
                    using (SoundPlayer player = new SoundPlayer())
                    {
                        string location = System.Environment.CurrentDirectory + "/OK.wav";
                        player.SoundLocation = location;
                        player.Play();
                    }
                    okCount++;
                    okCountLbl.Content = okCount;
                }
                else
                {
                    this.finalLbl.Content = "NG";
                    this.finalLbl.Foreground = new SolidColorBrush(Colors.Red);
                    using (SoundPlayer player = new SoundPlayer())
                    {
                        string location = System.Environment.CurrentDirectory + "/NG.wav";
                        player.SoundLocation = location;
                        player.Play();
                    }
                    ngCount++;
                    ngCountLbl.Content = ngCount;
                }
            }
            catch (Exception Err)
            {
                MessageBox.Show(Err.Message);
                LogSet(Err.Message);
            }
            finally
            {
                GC.Collect();
            }
            //计时结束
            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            tactLbl.Content = ts.TotalSeconds.ToString("0.000");
            insLog(strLog);
        }


        /// <summary>
        /// 设定规格用
        /// </summary>
        /// 需要加密码
        private void changeSpec_Click(object sender, RoutedEventArgs e)
        {
            button.IsEnabled = false;
            Save.IsEnabled = true;
            
            pass pas = new pass();
            pas.ShowDialog();

            Keyboard.ClearFocus();

            //规格可变化时界面变更
            for (int i =0;i<spectable.Rows.Count;i++)
            {
                specList[i].IsReadOnly = false;
                specList[i].Background = new SolidColorBrush(Colors.White);
            }


        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {

            for (int i = 0; i < spectable.Rows.Count; i++)
            {
                spectable.Rows[i][1] = specList[i].Text;
                specList[i].IsReadOnly = true;
                specList[i].Background = new SolidColorBrush(Colors.LightCyan);

                button.IsEnabled = true;
                Keyboard.Focus(usercontrol1);
            }
            FileRewrite(AppDomain.CurrentDomain.BaseDirectory, "//BackCab.csv");
            Save.IsEnabled = false;
        }

        //规格读取
        public static DataTable CsvReader(string path, string filename)
        {
            DataTable tab = new DataTable();
            string pCsvPath = path + filename;//文件路径
            try
            {
                String line;
                String[] split = null;
                DataRow row = null;
                StreamReader sr = new StreamReader(pCsvPath, System.Text.Encoding.Default);
                //创建与数据源对应的数据列 
                line = sr.ReadLine();
                split = line.Split(',');
                foreach (String colname in split)
                {
                    tab.Columns.Add(colname, System.Type.GetType("System.String"));
                }
                //将数据填入数据表 
                int j = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    j = 0;
                    row = tab.NewRow();
                    split = line.Split(',');
                    foreach (String colname in split)
                    {
                        row[j] = colname;
                        j++;
                    }
                    tab.Rows.Add(row);
                }
                sr.Close();
                //显示数据 
                // this.dataGridView1.DataSource = table.DefaultView;
            }
            catch (Exception Err)
            {
                MessageBox.Show(Err.Message);
                LogSet(Err.Message);
            }
            finally
            {
                GC.Collect();
            }
            return tab;
        }

        //异常记录
        private static void LogSet(string msg)
        {
            using (FileStream stream = new FileStream(@"Log.txt", FileMode.Append))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(string.Format(DateTime.Now.ToString() + "  {0}\r\n", msg));
                }
            }
        }

        //规格保存
        private void FileRewrite(string path, string filename)
        {
            string pCsvPath = path + filename;

            try
            {
                using (FileStream stream = new FileStream(pCsvPath, FileMode.OpenOrCreate))
                {
                    using (StreamWriter write = new StreamWriter(stream))
                    {
                        // C3List.Clear();
                        string str1 = spectable.Columns[0].ToString()+","+ spectable.Columns[1].ToString();
                        write.WriteLine(str1);
                        for (int i=0;i<spectable.Rows.Count;i++)
                        {
                            string str2 = spectable.Rows[i][0].ToString().Trim() + "," + spectable.Rows[i][1].ToString().Trim();
                            write.WriteLine(str2);
                        }
                        stream.Flush();
                        write.Close();
                        stream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                LogSet(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        //检查履历
        private void insLog(string str)
        {
            string currPath = System.Windows.Forms.Application.StartupPath;
            string logPath = currPath + "/logHougai/";
            if(!Directory.Exists(logPath))
            {
                System.IO.Directory.CreateDirectory(logPath);
            }

            //按年月存放文件夹
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            string folderName = logPath + currentTime.Year.ToString("D4") + currentTime.Month.ToString("D2");
            if (!Directory.Exists(folderName))
            {
                System.IO.Directory.CreateDirectory(folderName);
            }
            string fileName = folderName + "/" + currentTime.Day.ToString() + ".csv";
            //文件头
            string strHead = "Time,";
            for (int i =0;i< spectable.Rows.Count; i++)
            {
                strHead += spectable.Rows[i][0];
                strHead += ",";
            }
            if(!File.Exists(fileName))
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(strHead);
                    sw.WriteLine(currentTime.ToLongTimeString().ToString() + "," + str);
                    sw.Flush();
                }
            }
            //文件内容
            else
                using (FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(currentTime.ToLongTimeString().ToString() +"," + str);
                    sw.Flush();
                }
        }
    }
}
