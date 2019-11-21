using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S3Test.services
{
    public sealed class SpireExcel 
    {
        public Spire.Xls.Workbook workbook = null;
        public Spire.Xls.Worksheet WorkSheetName = null;

     
        public SpireExcel()
        {
            try
            {
                
               //Spire.License.LicenseProvider.SetLicenseKey("ErZT6NpjAQACkAXlvPksKKblif6+wabU+q4CqcwGsW/fhXY2OdsVVPLMO4W/5jgekQPJC8hEomyBnFSwubLJ2yvf2bXalgwWwYPm8yEr7sXdbC2wDkjd2yrBDzw5M3qSAgT68GD97/+FA+X3ApbJxqOC1zLzt3lSOEcmacpCoW2FOwVgwt942c7SA8LQYYmOqfvQIUeimLnuDH3WiSCzjqArbay5d7PvwnI+mBkrHvnrn/5lQR3xBrKgQmrWdmWPFokTSn2T7mCl+gwbO3jw75xg+p4IWekIfladd1jjpfAY+6njX359TEKT+S3d3ubAdWeV255IunEKWN2pEg9rxCxUOJpvQIg4KmP3mHqp0Qj7NdRIXwOLiClqHBRZk5Wjr7aQwNgDiKosUMZwxzhPsKQjmrqDT6Hl1R0hlGQljGvfqUeAm6VJj1ZohfxVqsrLobDZAhGv7drrN2f4IhDkBz3QouO/UWpUGU/vJDowZwH8dwzJZVOvQ8dpqm70pH3n+DEjlHvg+PYghJipihUPGX/bQYoMYjW332bG2h5T24rah+NSSqtn2XCYi1T70PqCbtHxqXYh9ARyEspcEn2NKuV398PF2zTraNtYNRAeqiQlxaSo1j4cIRKTL3J432eGmaGJfAK9k/T/d3JKilP4McqTPkVLDHXlBJ5yb0i6LTyM4h15p2SyXgvlbBFdODdR68d+/9CbZaMgbOz9tuISfnJiZsh0EJdJsg6Oj7g8B1XDHu/TE5q1LSTUDN5RVhecQInSLnyPFNEeJuXnK0KGgeaTs4Rs54mTMqYoh2Iz5MjdzeTs5/E0dNaS63p3iBL64bcQaQVc4YvIXcHi6ywmiHg/PMvuHQb7qSUPMsBk7xc/QwAL2IKA6YCZxWl/qESIPHGC+GlVSZAw7xerA7/eE+xwU26173ytEPNflg3XrqAboHakIYGCb0/51cGCwJuQs8MksYWQQNniRofBCTX6rwOdjkw0u0IkTgxQ4FQ2JP3x1UmYFNDwLBGM/HZpaoSmQsCLQ091ZQWVU2xYZdV4vpz2Eocf8zffdrbxwju+wFBovhn9mXgGMmpIhfU2QMxY1Xgp9W+zTZHnbz0pME1IE2kzD0zpX9YXdAEg9149UKb06ecFwJSY8HP1BUR+WePw6KBw/ZSw3mYYCHbOT1DP9MFv/ibyKAxm0nmHwCoIV0Txw+rad9h8H45+ASBegJXmOrqCiZaFI96TMdfeejL2YcoLUViIMNQvLcaMoJWjarW/2MyhjI6vcjKTq1uY+ERLDbGYca2MygQybPYZUVcHLKUlcK1GiWSWWLFrY+AY2pTDqqNWv9QzYmzeSEqaAh6wR+8wWXh+HgVk9v+uGLrIK4Ql1yDBz+gP6VooVuf0wZFGO+i+Dg81dYjMB0/q5qfnkqpvvYHmAA3ub0njylAtqg88B98vfB4kHMqhjs2U/X6PMieRQe+08/pQYNnRrf1f+zUXMJGjxv9FNyszCSEnWQqwpX/MTnh7WWtt6EFhtQcayCbEaUlpIdqA92UUh/vN");
                workbook = new Spire.Xls.Workbook();
                
                    
             
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}