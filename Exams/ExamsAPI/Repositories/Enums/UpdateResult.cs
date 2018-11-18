/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Enum for Exams API Update Result
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

namespace ExamsAPI.Repositories.Enums
{
    /// <summary>
    /// Enum for update result
    /// </summary>
    public enum UpdateResult
    {
        NotFound,
        NoAccess,
        Success,
        Fail,
        ExamEnded
    }
}