namespace #xOk
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    internal class #Tsk<#gi>
    {
        private #gi #Hyh;
        private Dictionary<#gi, int> #Rsk;
        private List<#gi> #lJ;
        private static bool #Ssk;

        public void #6e()
        {
            this.#Hyh = default(#gi);
            this.#Rsk = null;
            this.#lJ = null;
        }

        public unsafe int #FOk(#gi #G3)
        {
            int count;
            if (#G3.Equals(this.#Hyh))
            {
                return 0;
            }
            if (#Tsk<#gi>.#Ssk)
            {
                if (this.#Hyh == null)
                {
                    this.#Hyh = #G3;
                    return 0;
                }
            }
            else
            {
                #gi local;
                #gi* localPtr1 = &local;
                localPtr1 = default(#gi);
                if (localPtr1.Equals(this.#Hyh))
                {
                    this.#Hyh = #G3;
                    return 0;
                }
            }
            if (this.#Rsk == null)
            {
                this.#Rsk = new Dictionary<#gi, int>();
            }
            else if (this.#Rsk.TryGetValue(#G3, out count))
            {
                return count;
            }
            if (this.#lJ != null)
            {
                count = this.#lJ.Count;
                this.#Rsk[#G3] = count;
                this.#lJ.Add(#G3);
                return count;
            }
            this.#lJ = new List<#gi>(2);
            this.#lJ.Add(this.#Hyh);
            this.#lJ.Add(#G3);
            this.#Rsk[this.#Hyh] = 0;
            this.#Rsk[#G3] = 1;
            return 1;
        }

        static #Tsk()
        {
            #gi local = default(#gi);
            #Tsk<#gi>.#Ssk = local == null;
        }

        public #gi this[int #ahb] =>
            (this.#lJ != null) ? this.#lJ[#ahb] : this.#Hyh;
    }
}

