// Copyright (C) 2013  Joey Scarr
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.IO;
using System.Text;

namespace KFS.FileSystems {
	public class PathUtils {
		public static string Combine(params string[] paths) {
			// If the first path is a drive letter and colon (e.g. "C:"), append a
			// separator before calling Path.Combine.
			if (paths[0].Length == 2 && paths[0][1] == ':') {
				paths[0] += Path.DirectorySeparatorChar;
			}
			return Path.Combine(paths);
		}

		public static string MakeFileNameValid(string filename) {
			StringBuilder sb = new StringBuilder(filename);
			foreach (char c in Path.GetInvalidFileNameChars()) {
				sb.Replace(c, '_');
			}
			return sb.ToString();
		}
	}
}
