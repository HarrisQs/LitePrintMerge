using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeDataAndDoc
{
    [TestFixture]
    public class TestMerge
    {
        [Test]
        public void MergeTest()
        {
            StringReader input = new StringReader("身份證字號\t中文姓名\t年數\r\n" +
                                                  "A445\t張\t9");
            StringReader template = new StringReader("${中文姓名}先生(身份證字號${身份證字號})為本校專任教師，聘期${年數}。\r\n\t\t\t\t\t\t\t此聘                  \r\n\t\t\t\t\t\t\t\t校長\r\n");
            StringWriter output = new StringWriter();
            Program.MergeData(input, template, output);
            Assert.That(output.ToString(), Is.EqualTo("張先生(身份證字號A445)為本校專任教師，聘期9。\r\n\t\t\t\t\t\t\t此聘                  \r\n\t\t\t\t\t\t\t\t校長\r\n\r\n"));
        }
        [Test]
        public void MergeTest2()
        {
            StringReader input = new StringReader("身份證字號\t中文姓名\t年數\r\n" +
                                                  "A789455\t張非王\t90");
            StringReader template = new StringReader("${中文姓名}先生(身份證字號${身份證字號})為本校專任教師，聘期${年數}。\r\n\t\t\t\t\t\t\t此聘                  \r\n\t\t\t\t\t\t\t\t校長\r\n");
            StringWriter output = new StringWriter();
            Program.MergeData(input, template, output);
            Assert.That(output.ToString(), Is.EqualTo("張非王先生(身份證字號A789455)為本校專任教師，聘期90。\r\n\t\t\t\t\t\t\t此聘                  \r\n\t\t\t\t\t\t\t\t校長\r\n\r\n"));
        }
    }
}
