using Editor.CustomMarshal.CustomTypes;
using EVESharp.PythonTypes;
using EVESharp.PythonTypes.Types.Collections;
using EVESharp.PythonTypes.Types.Database;
using EVESharp.PythonTypes.Types.Primitives;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Editor
{
    public static class TreeViewPrettyPrinter
    {
        public static void Process(PyDataType data, TreeNode node)
        {

        }

        public static void Process(PyDataType data, out TreeNode node)
        {
            node = new TreeNode();

            ProcessPythonType(data, node);
        }

        private static void ProcessPythonType(PyDataType obj, TreeNode node)
        {
            switch (obj)
            {
                case null:
                case PyNone _: ProcessNone(node); break;
                case PyString pyString: ProcessString(pyString, node); break;
                case PyToken pyToken: ProcessToken(pyToken, node); break;
                case PyInteger pyInteger: ProcessInteger(pyInteger, node); break;
                case PyDecimal pyDecimal: ProcessDecimal(pyDecimal, node); break;
                case PyBuffer pyBuffer: ProcessBuffer(pyBuffer, node); break;
                case PyBool pyBool: ProcessBoolean(pyBool, node); break;
                case PyTuple pyTuple: ProcessTuple(pyTuple, node); break;
                case PyList pyList: ProcessList(pyList, node); break;
                case PyDictionary pyDictionary: ProcessDictionary(pyDictionary, node); break;
                case PyChecksumedStream pyChecksumedStream: ProcessChecksumedStream(pyChecksumedStream, node); break;
                case PyObject pyObject: ProcessObject(pyObject, node); break;
                case PyObjectData pyObjectData: ProcessObjectData(pyObjectData, node); break;
                case PyInsightSubStream pySubStream: ProcessSubStream(pySubStream, node); break;
                case PySubStruct pySubStruct: ProcessSubStruct(pySubStruct, node); break;
                case PyPackedRow pyPackedRow: ProcessPackedRow(pyPackedRow, node); break;
                default: node.Text = "[PyUnknown]"; break;
            }
        }

        private static void ProcessString(PyString str, TreeNode node)
        {
            node.Text = $"[PyString {str.Length} char(s): '{str.Value}']";
        }

        private static void ProcessToken(PyToken token, TreeNode node)
        {
            node.Text = $"[PyToken {token.Token.Length} bytes: '{token.Token}']";
        }

        private static void ProcessBoolean(PyBool boolean, TreeNode node)
        {
            node.Text = $"[PyBool {boolean.Value}]";
        }

        private static void ProcessInteger(PyInteger integer, TreeNode node)
        {
            node.Text = $"[PyInteger {integer.Value}]";
        }

        private static void ProcessDecimal(PyDecimal dec, TreeNode node)
        {
            node.Text = $"[PyDecimal {dec.Value}]";
        }

        private static void ProcessNone(TreeNode node)
        {
            node.Text = "[PyNone]";
        }

        private static void ProcessTuple(PyTuple tuple, TreeNode node)
        {
            node.Text = $"[PyTuple {tuple.Count} items]";

            // process all child elements
            foreach (PyDataType data in tuple)
            {
                Process(data, out TreeNode child);
                node.Nodes.Add(child);
            }
        }

        private static void ProcessList(PyList list, TreeNode node)
        {
            node.Text = $"[PyList {list.Count} items]";

            // process all child elements
            foreach (PyDataType data in list)
            {
                Process(data, out TreeNode child);
                node.Nodes.Add(child);
            }
        }

        private static void ProcessBuffer(PyBuffer buffer, TreeNode node)
        {
            node.Text = $"[PyBuffer {buffer.Length} bytes: {HexDump.ByteArrayToHexViaLookup32(buffer.Value)}]";
        }

        private static void ProcessDictionary(PyDictionary dictionary, TreeNode node)
        {
            node.Text = $"[PyDictionary {dictionary.Length} entries]";

            // process all the keys and values
            foreach (PyDictionaryKeyValuePair<PyDataType, PyDataType> pair in dictionary)
            {
                TreeNode child = new TreeNode();

                Process(pair.Key, out TreeNode key);
                Process(pair.Value, out TreeNode value);

                key.Nodes.Add(value);
                node.Nodes.Add(key);
            }
        }

        private static void ProcessChecksumedStream(PyChecksumedStream stream, TreeNode node)
        {
            node.Text = "[PyChecksumedStream]";

            Process(stream.Data, out TreeNode child);
            node.Nodes.Add(child);
        }

        private static void ProcessObject(PyObject obj, TreeNode node)
        {
            node.Text = $"[PyObject {((obj.IsType2) ? "Type2" : "Type1")}]";

            // process all object's parts
            Process(obj.Header, out TreeNode header);
            Process(obj.List, out TreeNode list);
            Process(obj.Dictionary, out TreeNode dictionary);

            node.Nodes.Add(header);
            node.Nodes.Add(list);
            node.Nodes.Add(dictionary);
        }

        private static void ProcessObjectData(PyObjectData data, TreeNode node)
        {
            node.Text = $"[PyObjectData {data.Name.Value}]";

            Process(data.Arguments, out TreeNode child);

            node.Nodes.Add(child);
        }

        private static void ProcessSubStream(PyInsightSubStream stream, TreeNode node)
        {
            node.Text = "[PySubStream]";

            Process(stream.Stream, out TreeNode child);

            node.Nodes.Add(child);
        }

        private static void ProcessSubStruct(PySubStruct subStruct, TreeNode node)
        {
            node.Text = "[PySubStruct]";

            Process(subStruct.Definition, out TreeNode child);

            node.Nodes.Add(child);
        }

        private static void ProcessPackedRow(PyPackedRow packedRow, TreeNode node)
        {
            node.Text = $"[PyPackedRow {packedRow.Header.Columns.Count} columns]";

            foreach (DBRowDescriptor.Column column in packedRow.Header.Columns)
            {
                TreeNode child = new TreeNode($"{column.Name}");

                Process(packedRow[column.Name], out TreeNode result);
                child.Nodes.Add(result);
                node.Nodes.Add(child);
            }
        }
    }
}