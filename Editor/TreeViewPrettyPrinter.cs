using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using PythonTypes;
using PythonTypes.Types.Collections;
using PythonTypes.Types.Database;
using PythonTypes.Types.Primitives;

namespace Editor
{
    public static class TreeViewPrettyPrinter
    {
        public static void Process(PyDataType data, TreeNode node)
        {
            ProcessPythonType(data, node);
        }

        private static TreeNode ProcessPythonType(PyDataType obj, TreeNode node)
        {
            switch (obj)
            {
                case null:
                case PyNone _: return ProcessNone(node);
                case PyString pyString: return ProcessString(pyString, node);
                case PyToken pyToken: return ProcessToken(pyToken, node); 
                case PyInteger pyInteger: return ProcessInteger(pyInteger, node);
                case PyDecimal pyDecimal: return ProcessDecimal(pyDecimal, node);
                case PyBuffer pyBuffer: return ProcessBuffer(pyBuffer, node);
                case PyBool pyBool: return ProcessBoolean(pyBool, node);
                case PyTuple pyTuple: return ProcessTuple(pyTuple, node);
                case PyList pyList: return ProcessList(pyList, node);
                case PyDictionary pyDictionary: return ProcessDictionary(pyDictionary, node);
                case PyChecksumedStream pyChecksumedStream: return ProcessChecksumedStream(pyChecksumedStream, node);
                case PyObject pyObject: return ProcessObject(pyObject, node);
                case PyObjectData pyObjectData: return ProcessObjectData(pyObjectData, node);
                case PySubStream pySubStream: return ProcessSubStream(pySubStream, node);
                case PySubStruct pySubStruct: return ProcessSubStruct(pySubStruct, node);
                case PyPackedRow pyPackedRow: return ProcessPackedRow(pyPackedRow, node);
                default: return node.Nodes.Add("[PyUnknown]");
            }
        }

        private static TreeNode ProcessString(PyString str, TreeNode node)
        {
            return node.Nodes.Add($"[PyString {str.Length} char(s): '{str.Value}']");
        }

        private static TreeNode ProcessToken(PyToken token, TreeNode node)
        {
            return node.Nodes.Add($"[PyToken {token.Token.Length} bytes: '{token.Token}']");
        }

        private static TreeNode ProcessBoolean(PyBool boolean, TreeNode node)
        {
            return node.Nodes.Add($"[PyBool {boolean.Value}]");
        }

        private static TreeNode ProcessInteger(PyInteger integer, TreeNode node)
        {
            return node.Nodes.Add($"[PyInteger {integer.Value}]");
        }

        private static TreeNode ProcessDecimal(PyDecimal dec, TreeNode node)
        {
            return node.Nodes.Add($"[PyDecimal {dec.Value}]");
        }

        private static TreeNode ProcessNone(TreeNode node)
        {
            return node.Nodes.Add("[PyNone]");
        }

        private static TreeNode ProcessTuple(PyTuple tuple, TreeNode node)
        {
            TreeNode child = node.Nodes.Add($"[PyTuple {tuple.Count} items]");

            // process all child elements
            foreach (PyDataType data in tuple)
                Process(data, child);

            return child;
        }

        private static TreeNode ProcessList(PyList list, TreeNode node)
        {
            TreeNode child = node.Nodes.Add($"[PyList {list.Count} items]");

            // process all child elements
            foreach (PyDataType data in list)
                Process(data, child);
            
            return child;
        }

        private static TreeNode ProcessBuffer(PyBuffer buffer, TreeNode node)
        {
            return node.Nodes.Add($"[PyBuffer {buffer.Length} bytes: {HexDump.ByteArrayToHexViaLookup32(buffer.Value)}]");
        }

        private static TreeNode ProcessDictionary(PyDictionary dictionary, TreeNode node)
        {
            TreeNode child = node.Nodes.Add($"[PyDictionary {dictionary.Length} entries]");

            // process all the keys and values
            foreach (PyDictionaryKeyValuePair<PyDataType, PyDataType> pair in dictionary)
            {
                TreeNode keyChild = ProcessPythonType(pair.Key, child);
                ProcessPythonType(pair.Value, keyChild);
            }

            return child;
        }

        private static TreeNode ProcessChecksumedStream(PyChecksumedStream stream, TreeNode node)
        {
            TreeNode child = node.Nodes.Add("[PyChecksumedStream]");

            ProcessPythonType(stream.Data, child);

            return child;
        }

        private static TreeNode ProcessObject(PyObject obj, TreeNode node)
        {
            TreeNode child = node.Nodes.Add($"[PyObject {((obj.IsType2) ? "Type2" : "Type1")}]");

            // process all object's parts
            Process(obj.Header, child.Nodes.Add("Header"));
            Process(obj.List, child.Nodes.Add("List"));
            Process(obj.Dictionary, child.Nodes.Add("Dictionary"));

            return child;
        }

        private static TreeNode ProcessObjectData(PyObjectData data, TreeNode node)
        {
            TreeNode child = node.Nodes.Add($"[PyObjectData {data.Name.Value}]");

            ProcessPythonType(data.Arguments, child);

            return child;
        }

        private static TreeNode ProcessSubStream(PySubStream stream, TreeNode node)
        {
            TreeNode child = node.Nodes.Add("[PySubStream]");

            ProcessPythonType(stream.Stream, child);

            return child;
        }

        private static TreeNode ProcessSubStruct(PySubStruct subStruct, TreeNode node)
        {
            TreeNode child = node.Nodes.Add("[PySubStruct]");

            ProcessPythonType(subStruct.Definition, child);

            return child;
        }

        private static TreeNode ProcessPackedRow(PyPackedRow packedRow, TreeNode node)
        {
            TreeNode child = node.Nodes.Add($"[PyPackedRow {packedRow.Header.Columns.Count} columns]");

            foreach (DBRowDescriptor.Column column in packedRow.Header.Columns)
            {
                ProcessPythonType(packedRow[column.Name], child.Nodes.Add($"{column.Name}"));
            }

            return child;
        }
    }
}