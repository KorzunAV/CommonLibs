using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Converters.HtmlToPdf;
using NUnit.Framework;

namespace Converters
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void test()
        {
            SelectPdfConverter.Convert(testhtml, @"http://normativka.by/", "test.pdf");
            
        }

        protected string testPth = @"d:\1\500066096_20150618.html";

        protected string testhtml = @"
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'>
            <html>
            <head runat='server'>
                <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                <base href='http://normativka.by/'>
                <link href='/nor/library/css/document-save-word.css' rel='stylesheet' type='text/css'/>
                <title></title>
            </head>
            <body marginwidth='0' marginheight='0' style='margin: 0; padding: 0;'>
            <div runat='server' id='bodyError'></div>
            <div runat='server' id='body'>
                <div id='content'>
                    <div id='canvasPanel'>
                        <div id='gutterPanel'>
                                
                                    <div runat='server' id='bodyPanel'>
                                        <div runat='server' id='bodyContent'>
                                <BODY><div><div class='demo'><p class='demo__title'>Документ отображен частично. Полный текст документа, который вы хотите посмотреть, находится в&nbsp;платном&nbsp;доступе&nbsp;к&nbsp;Библиотеке&nbsp;</p>
	<p>Для&nbsp;открытия полного доступа к&nbsp;материалам &laquo;Библиотеки&raquo;необходимо приобрести код доступа на нашем сайте или активировать его в разделе «Подписки».</p>

	<div class='demo__controls'>
	    <a class='btn btn_green' href='/services/price/'>Приобрести доступ</a>
		<a class='btn btn_blue' href='/trial/'>Получить пробный доступ</a>
		
	</div></div></div></BODY>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </body>
            </html>";

    }
}
