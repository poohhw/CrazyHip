using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace CrazyHip.Tool
{
    public class FTPManager
    {
        private string _ServerIP;  //ftp 서버 
        private string _UserID;    //ftp 계정
        private string _Password;  //ftp 비밀번호
        private FtpWebResponse _Response;
        private FtpWebRequest _Request;

        //생성자
        public FTPManager(string ServerIP, string UserID, string Password)
        {
            _ServerIP = ServerIP;
            _UserID = UserID;
            _Password = Password;
        }

        /// <summary>
        /// FTP 파일 업로드
        /// </summary>
        /// <param name="FolderName">FTP서버의 폴더명</param>
        /// <param name="FileName">FTP서버의 파일명</param>
        /// <param name="SendFullFilePath">업로드 할 파일명</param>
        /// <returns>반환값이 빈값이면 정상 완료, 있으면 Error</returns>
        public string FileUpload(string FolderName, string FileName, string SendFullFilePath)
        {
            string vReturn = string.Empty;

            Uri uri = new Uri("ftp://" + _ServerIP + "//" + FolderName.Replace("\\", "//") + "//" + FileName);
            _Request = (FtpWebRequest)WebRequest.Create(uri);
            _Request.Method = WebRequestMethods.Ftp.UploadFile;

            // This example assumes the FTP site uses anonymous logon.
            _Request.Credentials = new NetworkCredential(_UserID, _Password);

            FtpWebResponse response = (FtpWebResponse)_Request.GetResponse();

            try
            {
                using (FileStream fs = File.OpenRead(SendFullFilePath))
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    fs.Close();
                    Stream requestStream = _Request.GetRequestStream();
                    requestStream.Write(buffer, 0, buffer.Length);
                    requestStream.Flush();
                    requestStream.Close();
                }
            }
            catch (WebException ex)
            {
                vReturn = ex.Message;
            }

            return vReturn;

        }

        /// <summary>
        /// FTP 서버 폴더 생성.
        /// </summary>
        /// <param name="FolderName"></param>
        /// <param name="CreateFolderName"></param>
        /// <returns></returns>
        public string CreateFolder(string FolderName,string CreateFolderName)
        {
            Stream ftpStream = null;
            string vReturn = string.Empty;
            try
            {
                Uri uri = new Uri("ftp://" + _ServerIP + "//" + FolderName.Replace("\\", "//") + "//" + CreateFolderName);

                _Request = (FtpWebRequest)FtpWebRequest.Create(uri);
                _Request.Method = WebRequestMethods.Ftp.MakeDirectory;
                _Request.UseBinary = true;
                _Request.Credentials = new NetworkCredential(_UserID, _Password);

                _Response = (FtpWebResponse)_Request.GetResponse();
                ftpStream = _Response.GetResponseStream();
                ftpStream.Close();
                _Response.Close();
            }
            catch (WebException ex)
            {
                vReturn = ex.Message;
            }
            return vReturn;
        }
    }
}
