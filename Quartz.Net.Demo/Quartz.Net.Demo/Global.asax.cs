using log4net.Config;
using Quartz.Net.Demo.Codes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Quartz.Net.Demo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Ӧ�ó�������ʱ����log4net���� 
            XmlConfigurator.Configure();

            Log.Info("Application_Start ����");

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // ��ʼ��������ʱ��������
            QuartzUtil.Init();

            // �����趨������
            QuartzBase.Start();

            Log.Info("Application_Start �����趨������");
        }

        protected void Application_End(object sender, EventArgs e)
        {
            Log.Info("Application_End ����\r\n");
            Activate();
        }

        public static void Restart()
        {
            Log.Info("Restart()  ж�����ж�ʱ��... ");
            QuartzUtil.Shutdown(false);     //true �ȴ���ǰjobִ�����ٹرգ�false ֱ�ӹر�
            Log.Info("Restart()  ׼������ ");
            HttpRuntime.UnloadAppDomain();  //�ᴥ��Application_End �¼�
            Log.Info("Restart()  ������� ");

            //Activate();
        }


        /// <summary>
        /// ��������� 
        ///     IIS���պ󣬽�����Application_End�¼����跢��һ������Żᴥ�� Application_Start�¼���ʼִ�ж�ʱ����
        /// </summary>
        public static void Activate()
        {
            string host = System.Configuration.ConfigurationManager.AppSettings["WebUrl"].ToString();
            string url = host.TrimEnd('/') + "/Home/Ping";
            Log.Info("PING URL : " + url);
            string res = HttpUtils.HttpGet(url);
            Log.Info("PING RESULT:" + res + "\r\n");
        }
    }
}
