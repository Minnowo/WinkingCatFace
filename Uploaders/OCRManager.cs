using System;
using System.Drawing;
using System.Linq;
//using System.Windows.Forms;
using System.Threading;
using System.Net.Http;
using System.IO;
using WinkingCat.HelperLibs;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WinkingCat.Uploaders
{
    public class Rootobject
    {
        public Parsedresult[] ParsedResults { get; set; }
        public int OCRExitCode { get; set; }
        public bool IsErroredOnProcessing { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }
    }

    public class Parsedresult
    {
        public object FileParseExitCode { get; set; }
        public string ParsedText { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }
    }
    public enum OCRSpaceLanguages
    {
        [Description("Arabic")]
        ara,
        [Description("Bulgarian")]
        bul,
        [Description("Chinese (Simplified)")]
        chs,
        [Description("Chinese (Traditional)")]
        cht,
        [Description("Croatian")]
        hrv,
        [Description("Czech")]
        cze,
        [Description("Danish")]
        dan,
        [Description("Dutch")]
        dut,
        [Description("English")]
        eng,
        [Description("Finnish")]
        fin,
        [Description("French")]
        fre,
        [Description("German")]
        ger,
        [Description("Greek")]
        gre,
        [Description("Hungarian")]
        hun,
        [Description("Korean")]
        kor,
        [Description("Italian")]
        ita,
        [Description("Japanese")]
        jpn,
        [Description("Norwegian")]
        nor,
        [Description("Polish")]
        pol,
        [Description("Portuguese")]
        por,
        [Description("Russian")]
        rus,
        [Description("Slovenian")]
        slv,
        [Description("Spanish")]
        spa,
        [Description("Swedish")]
        swe,
        [Description("Turkish")]
        tur
    }
    public static class OCRManager
    {
        public static readonly int maxUploadSizeBytes = 1048576;
        public static readonly int englishLanguageIndex = 8;

        private static readonly string apiURL = "https://api.ocr.space/Parse/Image";

        private static async Task<string> Upload(string path, int index)
        {
            string result = "";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.Timeout = new TimeSpan(1, 1, 1);

                    using (MultipartFormDataContent form = new MultipartFormDataContent())
                    {
                        form.Add(new StringContent("helloworld"), "apikey"); //Added api key in form data
                        form.Add(new StringContent(((OCRSpaceLanguages)index).ToString()), "language");

                        form.Add(new StringContent("2"), "ocrengine");
                        form.Add(new StringContent("true"), "scale");
                        form.Add(new StringContent("true"), "istable");

                        byte[] imageData = File.ReadAllBytes(path);
                        form.Add(new ByteArrayContent(imageData, 0, imageData.Length), "image", "image.jpg");

                        using (HttpResponseMessage response = await httpClient.PostAsync(apiURL, form))
                        {

                            string strContent = await response.Content.ReadAsStringAsync();

                            Rootobject ocrResult = JsonConvert.DeserializeObject<Rootobject>(strContent);

                            if (ocrResult.OCRExitCode == 1)
                            {
                                for (int i = 0; i < ocrResult.ParsedResults.Count(); i++)
                                {
                                    result = ocrResult.ParsedResults[i].ParsedText;
                                }
                            }
                            else
                            {
                                result = "ERROR: " + strContent;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.WriteException(exception);
            }
            return result;
        }

        public static async Task<string> UploadImage(string path, int langugeIndex = 6)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path) || PathHelper.GetFileSizeBytes(path) > maxUploadSizeBytes)
                return string.Empty;
            langugeIndex = langugeIndex.Clamp(0, 19);

            return await Upload(path, langugeIndex);
        }

        public static async Task<string> UploadPDF(string path, int langugeIndex = 6)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path) || PathHelper.GetFileSizeBytes(path) > maxUploadSizeBytes)
                return string.Empty;
            langugeIndex = langugeIndex.Clamp(0, 19);

            return await Upload(path, langugeIndex);
        }
    }
}
