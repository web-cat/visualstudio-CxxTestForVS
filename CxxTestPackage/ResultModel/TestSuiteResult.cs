/*==========================================================================*\
 |  $Id$
 |*-------------------------------------------------------------------------*|
 |  Copyright (C) 2008 Virginia Tech
 |
 |  This file is part of the Web-CAT CxxTest integration package for Visual
 |	Studio.NET.
 |
 |  Web-CAT is free software; you can redistribute it and/or modify
 |  it under the terms of the GNU Affero General Public License as published
 |  by the Free Software Foundation; either version 3 of the License, or
 |  (at your option) any later version.
 |
 |  Web-CAT is distributed in the hope that it will be useful,
 |  but WITHOUT ANY WARRANTY; without even the implied warranty of
 |  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 |  GNU General Public License for more details.
 |
 |  You should have received a copy of the GNU Affero General Public License
 |  along with Web-CAT; if not, see <http://www.gnu.org/licenses/>.
\*==========================================================================*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace WebCAT.CxxTest.VisualStudio.ResultModel
{
	class TestSuiteResult : ITestResult
	{
		public TestSuiteResult(XmlAttributeCollection attributes)
		{
			children = new List<ITestSuiteChildResult>();

			name = attributes["name"].Value;
			filename = attributes["file"].Value;
			line = int.Parse(attributes["line"].Value);
		}
		
		public string Name
		{
			get
			{
				return name;
			}
		}
		
		public string Filename
		{
			get
			{
				return filename;
			}
		}
		
		public int LineNumber
		{
			get
			{
				return line;
			}
		}

		public ITestSuiteChildResult[] Children(bool onlyTestCases)
		{
			if(!onlyTestCases)
			{
				return children.ToArray();
			}
			else
			{
				List<ITestSuiteChildResult> list =
					new List<ITestSuiteChildResult>();

				foreach(ITestSuiteChildResult child in children)
					if(child is TestCaseResult)
						list.Add(child);

				return list.ToArray();
			}
		}
		
		public void AddChild(ITestSuiteChildResult child)
		{
			children.Add(child);
		}

		public ITestResult Parent
		{
			get
			{
				return null;
			}
		}

		public TestResultStatus Status
		{
			get
			{
				TestResultStatus maxStatus = TestResultStatus.OK;

				foreach(ITestSuiteChildResult child in children)
					if(child.Status > maxStatus)
						maxStatus = child.Status;
				
				return maxStatus;
			}
		}
		
		public override string ToString()
		{
			return Name;
		}

		private List<ITestSuiteChildResult> children;

		private string name;
		
		private string filename;
		
		private int line;
	}
}
