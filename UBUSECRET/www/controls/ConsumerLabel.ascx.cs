using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Main;

namespace www.controls
{
    public partial class ConsumerLabel : System.Web.UI.UserControl
    {
        private Secret secret;
        private User consumer;

        public Secret Secret { get => secret; set => secret = value; }
        public User Consumer { get => consumer; set => consumer = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (secret != null && consumer != null)
            {
                ConsumerName.Text = consumer.Name;
            }
        }

        protected void DeleteConsumer(object sender, EventArgs e)
        {
            secret.RemoveConsumer(consumer);
            Response.Redirect(Request.RawUrl);
        }
    }
}