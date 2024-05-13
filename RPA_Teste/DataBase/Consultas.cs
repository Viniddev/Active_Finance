using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA_Teste.DataBase
{
    internal class Consultas
    {
        public static string GetFundos(bool filtroAtivo = true) 
        {
            if (filtroAtivo)
            {
                return @"
                    SELECT NOME FROM [ACTIVE_FINANCE].[DBO].[FUNDOSIMOBILIARIOS]
                    WHERE [STATUS] = 1
                ";
            }
            else 
            {
                return @"
                    SELECT [NOME], [PRECODESEJADO], [STATUS] FROM [ACTIVE_FINANCE].[DBO].[FUNDOSIMOBILIARIOS]
                ";
            }

        }
        public static string GetAcoes(bool filtroAtivo = true)
        {
            if (filtroAtivo)
            {
                return @"
                    SELECT NOME FROM [ACTIVE_FINANCE].[DBO].[ACOES]
                    WHERE [STATUS] = 1
                ";
            }
            else 
            {
                return @"
                    SELECT [NOME], [PRECODESEJADO], [STATUS] FROM [ACTIVE_FINANCE].[DBO].[ACOES]
                ";
            }

        }
        public static string GetPriceActionsAnalitics() 
        {
            return @" 
                SELECT A.NOME, A.VALORATUAL, COALESCE(B.PRECODESEJADO, 0) AS PRECODESEJADO
                FROM ACTIVE_FINANCE.DBO.EXTRACOESACOES AS A
                LEFT JOIN ACTIVE_FINANCE.DBO.ACOES AS B ON A.NOME = B.NOME
                INNER JOIN (
                    SELECT NOME, MAX(LASTINSERTION) AS MaxData
                    FROM ACTIVE_FINANCE.DBO.EXTRACOESACOES
                    GROUP BY NOME
                ) AS MaxDates ON A.NOME = MaxDates.NOME AND A.LASTINSERTION = MaxDates.MaxData;

            ";
        }

        public static string GetPriceFundsAnalitics()
        {
            return @" 
                SELECT A.NOME, A.VALORATUAL, COALESCE(B.PRECODESEJADO, 0) AS PRECODESEJADO
                FROM ACTIVE_FINANCE.DBO.EXTRACOESFUNDOIMOBILIARIO AS A
                LEFT JOIN ACTIVE_FINANCE.DBO.FUNDOSIMOBILIARIOS AS B ON A.NOME = B.NOME
                INNER JOIN (
                    SELECT NOME, MAX(LASTINSERTION) AS MaxData
                    FROM ACTIVE_FINANCE.DBO.EXTRACOESFUNDOIMOBILIARIO
                    GROUP BY NOME
                ) AS MaxDates ON A.NOME = MaxDates.NOME AND A.LASTINSERTION = MaxDates.MaxData;
            ";
        }

        public static string UpdateAcoes(string nome, string preco, int status)
        {
            return $@" 
                UPDATE [ACTIVE_FINANCE].[DBO].[ACOES]
                    SET [PRECODESEJADO] = {preco},
                    [STATUS] = {status}
                WHERE NOME LIKE('{nome}')
            ";
        }
        public static string InsertAcoes(string nome, string preco, int status)
        {
            return $@" 
               INSERT INTO [ACTIVE_FINANCE].[DBO].[ACOES] VALUES(
	                '{nome}',
	                 {preco},
	                 {status}
                )
            ";
        }
        public static string UpdateFundos(string nome, string preco, int status)
        {
            return $@" 
                UPDATE [ACTIVE_FINANCE].[DBO].[FUNDOSIMOBILIARIOS]
                    SET [PRECODESEJADO] = {preco},
                    [STATUS] = {status}
                WHERE NOME LIKE('{nome}')
            ";
        }
        public static string InsertFundos(string nome, string preco, int status)
        {
            return $@" 
               INSERT INTO [ACTIVE_FINANCE].[DBO].[FUNDOSIMOBILIARIOS] VALUES(
	                '{nome}',
	                 {preco},
	                 {status}
                )
            ";
        }
    }
}