using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using IronWord;
using IronWord.Models;
using IronSoftware.Drawing;

namespace RPA_Teste.Pipes.Word
{
    internal class WriteWordDoc
    {
        public static void Write(string acoes, string fundos) 
        {
            string pathSaveFile = @$"{System.AppDomain.CurrentDomain.BaseDirectory}Output\Word\DocumentoTeste.docx";
            try 
            {
                var docx1 = new WordDocument();
                Text addText = new Text(acoes);
                docx1.AddParagraph(new Paragraph(addText));

                Text addText2 = new Text(fundos);
                docx1.AddParagraph(new Paragraph(addText2));

                docx1.Save(pathSaveFile);
            } catch 
            {
                Console.WriteLine("Não Gravou");
            }

            Telegram.TelegramApi.SendLogArchive(pathSaveFile, "Word.docx").Wait();
            
        }
    }
}
