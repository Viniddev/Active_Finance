using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA_Teste.DataBase
{
    internal class Consultas
    {
        public static string GetFundos() 
        {
            return @"
                SELECT NOME FROM [ACTIVE_FINANCE].[DBO].[FUNDOSIMOBILIARIOS]
                WHERE [STATUS] = 1
            ";
        }
        public static string GetAcoes()
        {
            return @"
                SELECT NOME FROM [ACTIVE_FINANCE].[DBO].[ACOES]
                WHERE [STATUS] = 1
            ";
        }
        public static string GetPriceAnalitics() 
        {
            return @" 
                SELECT A.NOME, A.VALORATUAL, B.PRECODESEJADO
                FROM ACTIVE_FINANCE.DBO.EXTRACOESACOES AS A
                LEFT JOIN ACTIVE_FINANCE.DBO.ACOES AS B ON A.NOME = B.NOME
                INNER JOIN (
                    SELECT NOME, MAX(LASTINSERTION) AS MaxData
                    FROM ACTIVE_FINANCE.DBO.EXTRACOESACOES
                    GROUP BY NOME
                ) AS MaxDates ON A.NOME = MaxDates.NOME AND A.LASTINSERTION = MaxDates.MaxData;
            ";
        }
    }
}