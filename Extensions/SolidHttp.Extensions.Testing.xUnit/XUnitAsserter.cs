using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Sdk;

namespace SolidHttp.Extensions.Testing.Xunit
{
    public class XUnitAsserter : IAsserter
    {
        public void AreEqual<T>(T expected, T actual, string message)
        {         
            PerformAssertion<EqualException>(() => Assert.Equal<T>(expected, actual), message);
        }

        public void AreEqual(object expected, object actual, string message)
        {
            PerformAssertion<EqualException>(() => Assert.Equal(expected, actual), message);
        }

        public void AreNotEqual<T>(T notExpected, T actual, string message)
        {
            PerformAssertion<NotEqualException>(() => Assert.NotEqual<T>(notExpected, actual), message);
        }

        public void AreNotEqual(object notExpected, object actual, string message)
        {
            PerformAssertion<NotEqualException>(() => Assert.NotEqual(notExpected, actual), message);
        }

        public void AreNotSame(object notExpected, object actual, string message)
        {
            PerformAssertion<NotSameException>(() => Assert.NotSame(notExpected, actual), message);
        }

        public void AreSame(object expected, object actual, string message)
        {
            PerformAssertion<SameException>(() => Assert.Same(expected, actual), message);
        }

        public void Fail(string message)
        {
            Assert.True(false, message);
        }

        public void IsFalse(bool value, string message)
        {
            Assert.False(value, message);
        }

        public void IsInstanceOfType<TExpected>(object actual, string message)
        {
            PerformAssertion<IsTypeException>(() => Assert.IsType<TExpected>(actual), message);
        }

        public void IsInstanceOfType(Type expected, object actual, string message)
        {
            PerformAssertion<IsTypeException>(() => Assert.IsType(expected, actual), message);
        }

        public void IsNotInstanceOfType<TNotExpected>(object actual, string message)
        {
            PerformAssertion<IsNotTypeException>(() => Assert.IsNotType<TNotExpected>(actual), message);
        }

        public void IsNotInstanceOfType(Type notExpected, object actual, string message)
        {
            PerformAssertion<IsNotTypeException>(() => Assert.IsType(notExpected, actual), message);
        }

        public void IsNotNull(object value, string message)
        {
            PerformAssertion<NotNullException>(() => Assert.NotNull(value), message);
        }

        public void IsNull(object value, string message)
        {
            PerformAssertion<NullException>(() => Assert.Null(value), message);
        }

        public void IsTrue(bool value, string message)
        {
            Assert.True(value, message);
        }

        private void PerformAssertion<TException>(Action assert, string message)
            where TException : Exception
        {
            var exception = null as TException;
            try
            {
                assert();
            }
            catch(TException ex)
            {
                exception = ex;
            }
            finally
            {
                Assert.True(exception == null, message);
            }
        }
    }
}
