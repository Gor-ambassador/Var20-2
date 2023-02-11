/*
 Массив данных о пассажирах теплохода:
 * ФИО пассажира,
 * номер каюты,
 * тип каюты (люкс, 1, 2, 3 классы),
 * порт назначения (сравнение по полям – номер каюты, порт назначения,ФИО)
 */

using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CreateBD20.DBStructure
{
    /// <summary>
    /// Class with data about passenger
    /// </summary>
    public class Info
    {
        /// <value>
        /// Service field for interaction with the database
        /// </value>
        public int InfoId { get; set; }
        /// <value>
        /// Passenger name
        /// </value>
        public string Name { get; set; }
        /// <value>
        /// Destination port
        /// </value>
        public string Port { get; set; }
        /// <value>
        /// Passenger room number
        /// </value>
        public int RoomNumber { get; set; }
        /// <value>
        /// Passenger room type
        /// </value>
        public string RoomType { get; set; }


        /// <summary>
        /// Operator < overloading
        /// </summary>
        /// <returns>
        /// True if left element is less to the right, else false
        /// </returns>
        /// <param name="left">
        /// Left element
        /// </param>
        /// /// <param name="right">
        /// Right element
        /// </param>
        public static bool operator<(Info left, Info right)
        {
            /*
            if (left.RoomNumber == right.RoomNumber)
            {
                if (left.Port == right.Port)
                {
                    if (left.Name == right.Name)
                        return false;
                    else if (string.Compare(left.Port, right.Port) < 0)
                        return true;
                    return false;
                }
                else if (string.Compare(left.Port, right.Port) < 0)
                    return true;
                return false;
            }
            else if (left.RoomNumber < right.RoomNumber)
                return true;

            return false;*/
            return String.Compare(left.Port, right.Port) < 0;
        }

        /// <summary>
        /// Operator > overloading
        /// </summary>
        /// <returns>
        /// True if left element is greater to the right, else false
        /// </returns>
        /// <param name="left">
        /// Left element
        /// </param>
        /// /// <param name="right">
        /// Right element
        /// </param>
        public static bool operator>(Info left, Info right)
        {/*
            if (left.RoomNumber == right.RoomNumber)
            {
                if (left.Port == right.Port)
                {
                    if (left.Name == right.Name)
                        return false;
                    else if (string.Compare(left.Name, right.Name) > 0)
                        return true;
                    return false;
                }
                else if (string.Compare(left.Port, right.Port) > 0)
                    return true;
                return false;
            }
            else if (left.RoomNumber > right.RoomNumber)
                return true;

            return false;
            */
            return String.Compare(left.Port, right.Port) > 0;
        }

        /// <summary>
        /// Operator <= overloading
        /// </summary>
        /// <returns>
        /// True if left element is less or equal to the right, else false
        /// </returns>
        /// <param name="left">
        /// Left element
        /// </param>
        /// /// <param name="right">
        /// Right element
        /// </param>
        public static bool operator <=(Info left, Info right)
        {
            /*
            if (left.RoomNumber == right.RoomNumber)
            {
                if (left.Port == right.Port)
                {
                    if (left.Name == right.Name)
                        return true;
                    else if (string.Compare(left.Name, right.Name) < 0)
                        return true;
                    return false;
                }
                else if (string.Compare(left.Port, right.Port) < 0)
                    return true;
                return false;
            }
            else if (left.RoomNumber < right.RoomNumber)
                return true;

            return false;
            */
            return String.Compare(left.Port, right.Port) <= 0;
        }

        /// <summary>
        /// Operator >= overloading
        /// </summary>
        /// <returns>
        /// True if left element is greater or equal to the right, else false
        /// </returns>
        /// <param name="left">
        /// Left element
        /// </param>
        /// /// <param name="right">
        /// Right element
        /// </param>
        public static bool operator >=(Info left, Info right)
        {
            /*
            if (left.RoomNumber == right.RoomNumber)
            {
                if (left.Port == right.Port)
                {
                    if (left.Name == right.Name)
                        return true;
                    else if (string.Compare(left.Name, right.Name) > 0)
                        return true;
                    return false;
                }
                else if (string.Compare(left.Port, right.Port) > 0)
                    return true;
                return false;
            }
            else if (left.RoomNumber > right.RoomNumber)
                return true;

            return false;
            */
            return String.Compare(left.Port, right.Port) >= 0;
        }
    }
}
