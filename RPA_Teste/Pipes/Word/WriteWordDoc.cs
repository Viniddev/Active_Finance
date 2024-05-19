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
        public static void Write(string message) 
        {
            try 
            {
                string pathSaveFile = @$"{System.AppDomain.CurrentDomain.BaseDirectory}Output\Word\DocumentoTeste.docx";
                
                var docx1 = new WordDocument();
                Text addText = new Text(message);

                docx1.AddParagraph(new Paragraph(addText));
                docx1.Save(pathSaveFile);
            } catch 
            {
                Console.WriteLine("Não Gravou");
            }
            
        }
    }
}
