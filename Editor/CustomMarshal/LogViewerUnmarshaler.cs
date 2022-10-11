using EVESharp.Types.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EVEmuTool.CustomMarshal
{
    internal class LogViewerUnmarshaler
    {

        private static void UnmarshalLogViewerPacket(string fulltext)
        {
            // get the selected string and parse it properly
            List<byte> bytes = new List<byte>();

            // if the selected area starts with something that's not ~ find it in the string
            if (fulltext.StartsWith((char)Specification.MARSHAL_HEADER) == false)
            {
                int index = fulltext.IndexOf((char)Specification.MARSHAL_HEADER);

                if (index == -1)
                {
                    MessageBox.Show("Cannot find the beginning of the marshal data. Are you sure you've selected a MarshalStream?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                fulltext = fulltext.Substring(index);
            }

            for (int i = 0; i < fulltext.Length; i++)
            {
                char current = fulltext[i];

                if (current == '\\')
                {
                    int indicatorIndex = i + 1;

                    if (indicatorIndex >= fulltext.Length)
                    {
                        bytes.Add(Encoding.ASCII.GetBytes(new char[] { current })[0]);
                        break;
                    }

                    // check next value
                    if (fulltext[indicatorIndex] == 'x')
                    {
                        int endIndex = indicatorIndex + 1 + 2;

                        if (endIndex >= fulltext.Length)
                            break;

                        string value = fulltext.Substring(indicatorIndex + 1, 2);

                        // okay, time to handle an hex number
                        byte number = byte.Parse(value, System.Globalization.NumberStyles.HexNumber);
                        bytes.Add(number);
                        i += 3;
                    }
                    else if (fulltext[indicatorIndex] == '\\')
                    {
                        bytes.Add(Encoding.ASCII.GetBytes("\\")[0]);
                        i++;
                    }
                    else if (fulltext[indicatorIndex] == 'n')
                    {
                        bytes.Add(Encoding.ASCII.GetBytes("\n")[0]);
                        i++;
                    }
                    else if (fulltext[indicatorIndex] == 'r')
                    {
                        bytes.Add(Encoding.ASCII.GetBytes("\r")[0]);
                        i++;
                    }
                    else if (fulltext[indicatorIndex] == 't')
                    {
                        bytes.Add(Encoding.ASCII.GetBytes("\t")[0]);
                        i++;
                    }
                }
                else
                {
                    bytes.Add(Encoding.ASCII.GetBytes(new char[] { current })[0]);
                }
            }

            byte[] marshal = bytes.ToArray();
            InsightUnmarshal unmarshal = null;

            try
            {
                unmarshal = PartialUnmarshal.ReadFromByteArray(marshal);
            }
            catch (UnmarshallException ex)
            {
                MessageBox.Show("Cannot fully parse the marshal stream, the provided data might be incomplete. Exception: " + ex.Message, "Important!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                unmarshal = ex.Unmarshal;
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot parse any data, make sure to select from the start of the MarshalStreams' value (~ onwards) till the end", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // TODO: SHOW THE WINDOW WITH THE UNMARSHALED DATA
            // use the marshal section for this
            // this.LoadFileDetails(marshal, unmarshal);
        }
    }
}
