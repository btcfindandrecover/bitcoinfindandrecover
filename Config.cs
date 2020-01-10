using System;
using System.Xml;
using System.Reflection;
using System.Configuration;
using System.Windows.Forms;

namespace CryptoFinder
{
    public class Config : System.Configuration.AppSettingsReader
    {
        private XmlNode node;
        private string _cfgFile;

        public string cfgFile
        {
            get { return _cfgFile; }
            set { _cfgFile = Application.StartupPath + "\\" + value; }
        }

        public string GetValue(string key)
        {
            return Convert.ToString(GetValue(key, typeof(string)));
        }

        public new object GetValue(string key, System.Type sType)
        {
            XmlDocument doc = new XmlDocument();
            object ro = String.Empty;
            loadDoc(doc);
            string sNode = key.Substring(0, key.LastIndexOf("//"));
            // retrieve the selected node
            try
            {
                node = doc.SelectSingleNode(sNode);
                if (node != null)
                {
                    // Xpath selects element that contains the key
                    XmlElement targetElem = (XmlElement)node.SelectSingleNode(key);
                    if (targetElem != null)
                    {
                        ro = targetElem.GetAttribute("value");
                    }
                }
                if (sType == typeof(string))
                    return Convert.ToString(ro);
                else
                    if (sType == typeof(bool))
                {
                    if (ro.Equals("True") || ro.Equals("False"))
                        return Convert.ToBoolean(ro);
                    else
                        return false;
                }
                else
                    if (sType == typeof(int))
                    return Convert.ToInt32(ro);
                else
                    if (sType == typeof(double))
                    return Convert.ToDouble(ro);
                else
                    if (sType == typeof(DateTime))
                    return Convert.ToDateTime(ro);
                else
                    return Convert.ToString(ro);
            }
            catch
            {
                return String.Empty;
            }
        }

        public bool SetValue(string key, string val)
        {
            XmlDocument doc = new XmlDocument();
            loadDoc(doc);
            try
            {
                // retrieve the target node
                string sNode = key.Substring(0, key.LastIndexOf("//"));
                node = doc.SelectSingleNode(sNode);
                if (node == null)
                    return false;
                // Set element that contains the key
                XmlElement targetElem = (XmlElement)node.SelectSingleNode(key);
                if (targetElem != null)
                {
                    // set new value
                    targetElem.SetAttribute("value", val);
                }
                // create new element with key/value pair and add it
                else
                {

                    sNode = key.Substring(key.LastIndexOf("//") + 2);

                    XmlElement entry = doc.CreateElement(sNode.Substring(0, sNode.IndexOf("[@")).Trim());
                    sNode = sNode.Substring(sNode.IndexOf("'") + 1);

                    entry.SetAttribute("key", sNode.Substring(0, sNode.IndexOf("'")));

                    entry.SetAttribute("value", val);
                    node.AppendChild(entry);
                }
                saveDoc(doc, this._cfgFile);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void saveDoc(XmlDocument doc, string docPath)
        {
            // save document
            // choose to ignore if web.config since it may cause server sessions interruptions
            if (this._cfgFile.Equals("web.config"))
                return;
            else
                try
                {
                    XmlTextWriter writer = new XmlTextWriter(docPath, null);
                    writer.Formatting = Formatting.Indented;
                    doc.WriteTo(writer);
                    writer.Flush();
                    writer.Close();
                    return;
                }
                catch
                { }
        }

        public bool removeElement(string key)
        {
            XmlDocument doc = new XmlDocument();
            loadDoc(doc);
            try
            {
                string sNode = key.Substring(0, key.LastIndexOf("//"));
                // retrieve the appSettings node
                node = doc.SelectSingleNode(sNode);
                if (node == null)
                    return false;
                // XPath select setting "add" element that contains this key to remove
                XmlNode nd = node.SelectSingleNode(key);
                node.RemoveChild(nd);
                saveDoc(doc, this._cfgFile);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private void loadDoc(XmlDocument doc)
        {
            // check for type of config file being requested
            /*
			if(  this._cfgFile.Equals("app.config"))
			{
				// use default app.config
				this._cfgFile = ((Assembly.GetEntryAssembly()).GetName()).Name+".exe.config";
			}
			else
				if(  this._cfgFile.Equals("web.config"))
			{
				// use server web.config
				this._cfgFile = System.Web.HttpContext.Current.Server.MapPath("web.config");
			}
			*/
            // load the document

            doc.Load(this._cfgFile);
        }

    }
}
