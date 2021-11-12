using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Main;
using Data;

namespace www.controls
{
    public partial class ConsumerLabel : System.Web.UI.UserControl
    {
        private DB db;

        private Secret secret;
        private User consumer;
        private EventHandler onRemove;

        public Secret Secret { get => secret; set => secret = value; }
        public User Consumer { get => consumer; set => consumer = value; }
        public EventHandler OnRemove { get => onRemove; set => onRemove = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            db = DB.GetInstance();

            if (Secret != null && Consumer != null)
            {
                ConsumerName.Text = Consumer.Name;
            }
        }

        protected void DeleteConsumer(object sender, EventArgs e)
        {
            Secret.RemoveConsumer(Consumer);
            AppLogs.DetatchConsumer(Consumer, Secret);
            OnRemove?.Invoke(sender, e);
        }
    }
}