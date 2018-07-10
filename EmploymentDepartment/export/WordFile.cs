using System.Data;
using Word = Microsoft.Office.Interop.Word;

namespace EmploymentDepartment
{
    class WordFile
    {
        Word.Application app;
        Word.Document document;

        public WordFile(string templatePath)
        {
            app = new Word.Application();
            app.Visible = false;
            app.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;

            document = app.Documents.Open(templatePath);
        }

        public void ReplaceWordText(string textToReplace, string text)
        {
            var range = document.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: textToReplace, ReplaceWith: text);
        }                                                       
        
        public void Save(string filename)
        {
            document.SaveAs(filename);
            document.Close();
            app.Quit();
        }
    }
}
