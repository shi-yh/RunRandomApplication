using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace ConsoleApp1
{
    class Program
    {
        /// <summary>
        /// 随机启动项目
        /// </summary>
        public class RandomSelect
        {
            private string[] applicationPath=new string[2];
            /// <summary>
            /// 存储路径
            /// </summary>
            const string savePtah ="D://save";


            public string RandomStart()
            {
                applicationPath = GetPath();

                Random rd = new Random();

                int idx = rd.Next(0, applicationPath.Length);

                return applicationPath[idx];  
            }

            public void ResetData()
            {
                if (File.Exists(savePtah))
                {
                    File.Delete(savePtah);
                }
            }



            private string[] GetPath()
            {
                if (File.Exists(savePtah))
                {
                    applicationPath = File.ReadAllText(savePtah).Split('\n');
                }
                else
                {
                    for (int i = 0; i < applicationPath.Length; i++)
                    {
                       applicationPath[i]=OpenFolder(i);

                        if (string.IsNullOrEmpty(applicationPath[i]))
                        {
                            i--;
                        }
                    }

                    string str = applicationPath[0];

                    for (int i = 1; i < applicationPath.Length; i++)
                    {
                        str += '\n' + applicationPath[i];
                    }

                    StreamWriter sw = File.AppendText(savePtah);
                    sw.Write(str);
                    sw.Flush();
                    sw.Close();
                }

                return applicationPath;
            }

            private string OpenFolder(int idx)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Multiselect = true;//该值确定是否可以选择多个文件
                dialog.Title = "请选择第"+idx+"个文件";
                dialog.Filter = "所有文件(*.*)|*.*";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    return dialog.FileName;
                }
                else
                {
                    return "";
                }
            }
        }

        [STAThread]
        static void Main(string[] args)
        {

            RandomSelect rs = new RandomSelect();

            //rs.ResetData();

            Process process = new Process();

            process.StartInfo.FileName = rs.RandomStart();

            try
            {
                process.Start();
            }
            catch (Exception)
            {
                rs.ResetData();
                throw;
            }

        }


      
    }

   
}
