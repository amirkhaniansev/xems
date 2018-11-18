/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Class of Exams API Globals
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

using XemsLogger;
using XemsMailer.Mailers;
using ExamsAPI.Repositories.Interfaces;

namespace ExamsAPI.Global
{
    /// <summary>
    /// Class for global values of ExamsAPI
    /// </summary>
    public static class Globals
    {        
        /// <summary>
        /// Gets or sets logger
        /// </summary>
        public static IXemsLogger Logger { get; set; }

        /// <summary>
        /// Gets or sets mailer
        /// </summary>
        public static MessageMailer Mailer { get; set; }

        /// <summary>
        /// Gets or sets exam repository
        /// </summary>
        public static IExamRepository ExamRepository { get; set; }

        /// <summary>
        /// Gets or sets exam answers repository
        /// </summary>
        public static IExamAnswerRepository ExamAnswerRepository { get; set; }
    }
}