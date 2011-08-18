﻿namespace Twitterizer2.TestCases
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization.Formatters.Binary;
    using NUnit.Framework;
    using Twitterizer;

    [TestFixture]
    public class CoreTests
    {
        [Test]
        [Category("ReadOnly")]
        public static void Serialization()
        {
            Assembly twitterizerAssembly = Assembly.GetAssembly(typeof(TwitterUser));
            var objectTypesToCheck = from t in twitterizerAssembly.GetExportedTypes()
                               where
                               // TwitterIdCollection was never serializable
                               t != typeof(TwitterIdCollection) &&
                               // TwitterCursorPagedIdCollection was never serializable
                               t != typeof(TwitterCursorPagedIdCollection) &&
                               // UserIdCollection was never serializable
                               t != typeof(UserIdCollection) &&
                               !t.IsAbstract &&
                               !t.IsInterface &&
                               (
                                t.GetInterfaces().Contains(twitterizerAssembly.GetType("Twitterizer.Core.ITwitterObject")) ||
                                t.IsSubclassOf(twitterizerAssembly.GetType("Twitterizer.Entities.TwitterEntity"))
                               )
                               select t;

            foreach (Type type in objectTypesToCheck)
            {
                Console.WriteLine(string.Format("Inspecting: {0}", type.FullName));

                // Check that the object itself is marked as serializable
                Assert.That(type.IsSerializable, string.Format("{0} is not marked as Serializable", type.Name));

                // Get the parameter-less public constructor, if there is one
                ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);

                if (constructor == null)
                {
                    Console.WriteLine("{0} does not have a default public constructor", type.FullName);
                    constructor = type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);
                    Assert.IsNotNull(constructor, "{0} does not have a default constructor", type.FullName);
                }
                

                // Instantiate the type by invoking the constructor
                object objectToSerialize = constructor.Invoke(null);

                try
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        // Serialize the object
                        new BinaryFormatter().Serialize(ms, objectToSerialize);
                    }
                }
                catch (Exception) // Catch any exceptions and assert a failure
                {
                    Assert.Fail(string.Format("{0} could not be serialized", type.FullName));
                }
            }
        }

        [Test]
        [Category("ReadOnly")]
        public static void SSL()
        {
            TwitterResponse<TwitterUser> sslUser = TwitterUser.Show("twitterapi", new OptionalProperties() { UseSSL = true });
            Assert.That(sslUser.RequestUrl.StartsWith("https://"));
            
            TwitterResponse<TwitterUser> user = TwitterUser.Show("twitterapi", new OptionalProperties() { UseSSL = false });
            Assert.That(user.RequestUrl.StartsWith("http://"));

            TwitterResponse<TwitterStatusCollection> timeline = TwitterTimeline.HomeTimeline(Configuration.GetTokens(), new TimelineOptions() { UseSSL = true });
            Assert.That(timeline.RequestUrl.StartsWith("https://"));
            Assert.That(user.RequestUrl.StartsWith("https://"));
        }

        [Test]
        [Category("Distribution")]
        public static void CheckAssemblySignature()
        {
Assembly asm = Assembly.GetAssembly(typeof(TwitterUser));
if (asm != null)
{
    AssemblyName asmName = asm.GetName();
    byte[] key = asmName.GetPublicKey();
    Assert.That(key.Length > 0);
}
        }
    }
}
