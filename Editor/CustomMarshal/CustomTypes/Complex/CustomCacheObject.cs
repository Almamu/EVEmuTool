﻿using EVESharp.EVE.Packets.Complex;
using EVESharp.Types;
using EVESharp.Types.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVEmuTool.CustomMarshal.CustomTypes.Complex
{
    /// <summary>
    /// Helper class to work with PyCachedObject's (objectCaching.CachedObject) to be sent to the EVE Online client
    /// when cache requests are performed
    /// </summary>
    public class CustomCachedObject
    {
        private const string TYPE_NAME = "objectCaching.CachedObject";

        /// <summary>
        /// The datetime this cached object was generated
        /// </summary>
        public long Timestamp { get; private set; }
        /// <summary>
        /// The version (CRC32) of this cached object
        /// </summary>
        public int Version { get; private set; }
        /// <summary>
        /// The node that generated this object
        /// </summary>
        public long NodeID { get; private set; }
        /// <summary>
        /// Whether this cached object is shared between nodes or not
        /// </summary>
        public int Shared { get; private set; }
        /// <summary>
        /// The cached contents
        /// </summary>
        public PyBuffer Cache { get; private set; }
        /// <summary>
        /// The length in bytes of the cached object
        /// </summary>
        public int Compressed { get; private set; }
        /// <summary>
        /// ObjectID representation of the cached object to identify it
        /// </summary>
        public PyDataType ObjectID { get; private set; }

        public static implicit operator PyDataType(CustomCachedObject cachedObject)
        {
            if (cachedObject.Cache is null)
                throw new Exception("Cache data is null");

            if (cachedObject.ObjectID is null)
                throw new Exception("objectID is null");

            PyTuple version = new PyTuple(2)
            {
                [0] = cachedObject.Timestamp,
                [1] = cachedObject.Version
            };

            PyTuple args = new PyTuple(7)
            {
                [0] = version,
                [1] = null,
                [2] = cachedObject.NodeID,
                [3] = cachedObject.Shared,
                [4] = cachedObject.Cache,
                [5] = cachedObject.Compressed,
                [6] = cachedObject.ObjectID
            };


            return new PyObjectData(TYPE_NAME, args);
        }

        public static implicit operator CustomCachedObject(PyDataType cachedObject)
        {
            CustomCachedObject result = new CustomCachedObject();

            if (cachedObject is PyObjectData == false)
                throw new Exception("Expected PyObjectData");

            PyObjectData data = cachedObject as PyObjectData;

            if (data.Arguments is PyTuple == false)
                throw new Exception("PyObjectData's arguments must be Tuple");

            PyTuple args = data.Arguments as PyTuple;

            if (args.Count != 7)
                throw new Exception("PyObjectData's arguments must have 7 elements");

            PyDataType version = args[0];

            if (version is PyTuple == false)
                throw new Exception("Version content is not a Tuple");

            PyTuple versionTuple = version as PyTuple;

            if (versionTuple.Count != 2)
                throw new Exception("Version information does not have 2 elements in it");

            if (versionTuple[0] is PyInteger == false)
                throw new Exception("Timestamp is not correct, must be integer");

            if (versionTuple[1] is PyInteger == false)
                throw new Exception("Version is not correct, must be integer");

            result.Timestamp = versionTuple[0] as PyInteger;
            result.Version = versionTuple[0] as PyInteger;

            if (args[1] is not null == true)
                throw new Exception("Second arg is not none");

            if (args[2] is PyInteger == false)
                throw new Exception("Node ID is not integer");
            if (args[3] is PyInteger == false)
                throw new Exception("Shared is not integer");
            if (args[4] is PyByteString)
                result.Cache = new PyBuffer((args[4] as PyByteString).ByteData);
            if (args[4] is PyBuffer)
                result.Cache = args[4] as PyBuffer;
            if (result.Cache is null)
                throw new Exception("Cache data not loaded");

            if (args[5] is PyInteger)
                result.Compressed = args[5] as PyInteger;
            if (args[5] is PyBool)
                result.Compressed = (args[5] as PyBool).Value ? 1 : 0;

            result.NodeID = args[2] as PyInteger;
            result.ObjectID = args[6];

            return result;
        }
    }
}
