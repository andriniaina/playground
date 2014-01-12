using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace andri.TestUtilities.DataMocks
{
    class Class2 : IDataReader, IList<IDictionary<string,object>>
    {
        private bool _closed;
        void IDataReader.Close()
        {
            this._closed = true;
        }

        int IDataReader.Depth
        {
            get { throw new NotImplementedException(); }
        }

        DataTable IDataReader.GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        bool IDataReader.IsClosed
        {
            get { return this._closed; }
        }

        bool IDataReader.NextResult()
        {
            throw new NotImplementedException();
        }

        bool IDataReader.Read()
        {
            throw new NotImplementedException();
        }

        int IDataReader.RecordsAffected
        {
            get { throw new NotImplementedException(); }
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        int IDataRecord.FieldCount
        {
            get { throw new NotImplementedException(); }
        }

        bool IDataRecord.GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        byte IDataRecord.GetByte(int i)
        {
            throw new NotImplementedException();
        }

        long IDataRecord.GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        char IDataRecord.GetChar(int i)
        {
            throw new NotImplementedException();
        }

        long IDataRecord.GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        IDataReader IDataRecord.GetData(int i)
        {
            throw new NotImplementedException();
        }

        string IDataRecord.GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        DateTime IDataRecord.GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        decimal IDataRecord.GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        double IDataRecord.GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        Type IDataRecord.GetFieldType(int i)
        {
            throw new NotImplementedException();
        }

        float IDataRecord.GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        Guid IDataRecord.GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        short IDataRecord.GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        int IDataRecord.GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        long IDataRecord.GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        string IDataRecord.GetName(int i)
        {
            throw new NotImplementedException();
        }

        int IDataRecord.GetOrdinal(string name)
        {
            throw new NotImplementedException();
        }

        string IDataRecord.GetString(int i)
        {
            throw new NotImplementedException();
        }

        object IDataRecord.GetValue(int i)
        {
            throw new NotImplementedException();
        }

        int IDataRecord.GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        bool IDataRecord.IsDBNull(int i)
        {
            throw new NotImplementedException();
        }

        object IDataRecord.this[string name]
        {
            get { throw new NotImplementedException(); }
        }

        object IDataRecord.this[int i]
        {
            get { throw new NotImplementedException(); }
        }

        int IList<IDictionary<string, object>>.IndexOf(IDictionary<string, object> item)
        {
            throw new NotImplementedException();
        }

        void IList<IDictionary<string, object>>.Insert(int index, IDictionary<string, object> item)
        {
            throw new NotImplementedException();
        }

        void IList<IDictionary<string, object>>.RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IDictionary<string, object> IList<IDictionary<string, object>>.this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        void ICollection<IDictionary<string, object>>.Add(IDictionary<string, object> item)
        {
            throw new NotImplementedException();
        }

        void ICollection<IDictionary<string, object>>.Clear()
        {
            throw new NotImplementedException();
        }

        bool ICollection<IDictionary<string, object>>.Contains(IDictionary<string, object> item)
        {
            throw new NotImplementedException();
        }

        void ICollection<IDictionary<string, object>>.CopyTo(IDictionary<string, object>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        int ICollection<IDictionary<string, object>>.Count
        {
            get { throw new NotImplementedException(); }
        }

        bool ICollection<IDictionary<string, object>>.IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        bool ICollection<IDictionary<string, object>>.Remove(IDictionary<string, object> item)
        {
            throw new NotImplementedException();
        }

        IEnumerator<IDictionary<string, object>> IEnumerable<IDictionary<string, object>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
