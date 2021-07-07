using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinkingCat.HelperLibs
{
    public interface IUndoable
    {
        void Undo();

        void Redo();
    }
}
