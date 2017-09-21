using Lpp.Scanner.DataMart.Model.Processors.DataSetMapping;
using pSCANNER.DataMart.Model.processor.DataSetMapping.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace pSCANNER.DataMart.Model.processor.DataSetMapping {

    /// <summary>
    /// </summary>
    /// <seealso cref="Lpp.Scanner.DataMart.Model.Processors.DataSetMapping.IRegistry"/>
    internal class pSCANNER_R_Registry : IRegistry {

        /// <summary>
        ///     Gets the data set connection element map.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Dictionary<string, IDataSetConnection> GetDataSetConnectionElementMap() {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets the r section.
        /// </summary>
        /// <param name="typeIs"></param>
        /// <returns></returns>
        /// <exception cref="RSectionNotDefined"></exception>
        public string getRSection(TypeIs typeIs) {
            StringBuilder sb = new StringBuilder();

            switch (typeIs) {
                case TypeIs.Aggregator: {
                    sb.AppendLine("glm.sitedetail <- summary(glm.site);");
                    sb.AppendLine("x_ <- model.matrix(glm.site$terms);");
                    sb.AppendLine("p1 <-coefficients%*%t(x_)");
                    sb.AppendLine("p <-family(glm.s)$linkinv(eta = p1)");
                    sb.AppendLine("w <-diag(c(p*(1-p)));");
                    sb.AppendLine("glm.sitedetail$gradient <- t(x_)%*%(y-p);");
                    sb.AppendLine("glm.sitedetail$cov.new <- t(x_)%*%w%*%x;");
                    break;
                }
                case TypeIs.Analytics: {
                    //param.setPmmlCommand("glmObj");
                    //param.setConfidenceIntervalCommand("confint(glmObj)");
                    //if (family != null) {
                    //    param.addLine(String.format("glmObj <- glm(%s ~ %s, data=%s, family=\"%s\")", dependentVar, independentVars, DataObjectVariableName, family));
                    //} else {
                    //    param.addLine(String.format("glmObj <- glm(%s ~ %s, data=%s)", dependentVar, independentVars, DataObjectVariableName));
                    //}
                    break;
                }

                default: {
                    throw new RSectionNotDefined(typeIs);
                }
            }

            return sb.ToString();
        }
    }

    /// <summary>
    /// </summary>
    /// <seealso cref="System.Exception"/>
    [Serializable]
    internal class RSectionNotDefined : Exception {

        /// <summary>
        ///     Initializes a new instance of the <see cref="RSectionNotDefined"/> class.
        /// </summary>
        /// <param name="typeIs">The type is.</param>
        public RSectionNotDefined(TypeIs typeIs) : base(typeIs.ToString()) {
        }
    }
}