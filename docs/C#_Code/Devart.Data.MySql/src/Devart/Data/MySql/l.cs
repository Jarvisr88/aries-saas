namespace Devart.Data.MySql
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    internal class l
    {
        [SuppressUnmanagedCodeSecurity]
        internal class a
        {
            public static string a();
            public static string a(IntPtr A_0);
            public static IntPtr a(IntPtr A_0, bool A_1);
            public static byte a(IntPtr A_0, byte[] A_1, bool A_2);
            public static IntPtr a(IntPtr A_0, byte[] A_1, int A_2, bool A_3);
            public static int a(IntPtr A_0, byte[] A_1, int A_2, long A_3, bool A_4);
            public static int a(IntPtr A_0, int A_1, byte[] A_2, long A_3, bool A_4);
            public static string b();
            public static string b(IntPtr A_0);
            public static IntPtr b(IntPtr A_0, bool A_1);
            public static byte b(IntPtr A_0, byte[] A_1, bool A_2);
            public static string c(IntPtr A_0);
            public static int c(IntPtr A_0, bool A_1);
            public static string d(IntPtr A_0);
            public static int d(IntPtr A_0, bool A_1);
            public static string e(IntPtr A_0);
            public static int e(IntPtr A_0, bool A_1);
            [DllImport("kernel32.dll")]
            public static extern bool FreeLibrary(IntPtr A_0);
            [DllImport("kernel32.dll")]
            public static extern IntPtr GetProcAddress(IntPtr A_0, string A_1);
            [DllImport("kernel32.dll")]
            public static extern IntPtr LoadLibrary(string A_0);
            [DllImport("libmysql.dll")]
            public static extern long mysql_affected_rows(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern byte mysql_autocommit(IntPtr A_0, int A_1);
            [DllImport("libmysql.dll")]
            private static extern byte mysql_bind_param(IntPtr A_0, byte[] A_1);
            [DllImport("libmysql.dll")]
            private static extern byte mysql_bind_result(IntPtr A_0, byte[] A_1);
            [DllImport("libmysql.dll")]
            public static extern byte mysql_change_user(IntPtr A_0, string A_1, string A_2, string A_3);
            [DllImport("libmysql.dll")]
            private static extern IntPtr mysql_character_set_name(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern void mysql_close(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern byte mysql_commit(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern IntPtr mysql_connect(IntPtr A_0, string A_1, string A_2, string A_3);
            [DllImport("libmysql.dll")]
            public static extern void mysql_data_seek(IntPtr A_0, long A_1);
            [DllImport("libmysql.dll")]
            public static extern void mysql_debug(string A_0);
            [DllImport("libmysql.dll")]
            public static extern byte mysql_eof(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern int mysql_errno(IntPtr A_0);
            [DllImport("libmysql.dll")]
            private static extern IntPtr mysql_error(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern int mysql_escape_string(byte[] A_0, string A_1, int A_2);
            [DllImport("libmysql.dll")]
            private static extern int mysql_execute(IntPtr A_0);
            [DllImport("libmysql.dll")]
            private static extern int mysql_fetch(IntPtr A_0);
            [DllImport("libmysql.dll")]
            private static extern int mysql_fetch_column(IntPtr A_0, byte[] A_1, int A_2, long A_3);
            [DllImport("libmysql.dll")]
            public static extern IntPtr mysql_fetch_field(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern IntPtr mysql_fetch_field_direct(IntPtr A_0, int A_1);
            [DllImport("libmysql.dll")]
            public static extern IntPtr mysql_fetch_fields(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern IntPtr mysql_fetch_lengths(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern IntPtr mysql_fetch_row(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern int mysql_field_count(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern int mysql_field_seek(IntPtr A_0, int A_1);
            [DllImport("libmysql.dll")]
            public static extern int mysql_field_tell(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern void mysql_free_result(IntPtr A_0);
            [DllImport("libmysql.dll")]
            private static extern IntPtr mysql_get_client_info();
            [DllImport("libmysql.dll")]
            public static extern IntPtr mysql_get_host_info(IntPtr A_0);
            [DllImport("libmysql.dll")]
            private static extern IntPtr mysql_get_metadata(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern uint mysql_get_proto_info(IntPtr A_0);
            [DllImport("libmysql.dll", EntryPoint="mysql_get_proto_info")]
            public static extern uint mysql_get_proto_info_411(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern IntPtr mysql_get_server_info(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern IntPtr mysql_info(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern IntPtr mysql_init(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern long mysql_insert_id(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern int mysql_kill(IntPtr A_0, int A_1);
            [DllImport("libmysql.dll")]
            public static extern byte mysql_more_results(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern int mysql_next_result(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern int mysql_num_fields(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern long mysql_num_rows(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern int mysql_options(IntPtr A_0, ae A_1, IntPtr A_2);
            [DllImport("libmysql.dll")]
            public static extern int mysql_options(IntPtr A_0, ae A_1, ref int A_2);
            [DllImport("libmysql.dll")]
            private static extern int mysql_param_count(IntPtr A_0);
            [DllImport("libmysql.dll")]
            private static extern IntPtr mysql_param_result(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern int mysql_ping(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern IntPtr mysql_prepare(IntPtr A_0, string A_1, int A_2);
            [DllImport("libmysql.dll")]
            private static extern IntPtr mysql_prepare(IntPtr A_0, byte[] A_1, int A_2);
            [DllImport("libmysql.dll")]
            public static extern int mysql_query(IntPtr A_0, string A_1);
            [DllImport("libmysql.dll")]
            public static extern int mysql_query(IntPtr A_0, byte[] A_1);
            [DllImport("libmysql.dll")]
            public static extern IntPtr mysql_real_connect(IntPtr A_0, string A_1, string A_2, string A_3, string A_4, int A_5, string A_6, int A_7);
            [DllImport("libmysql.dll")]
            public static extern int mysql_real_escape_string(IntPtr A_0, byte[] A_1, string A_2, int A_3);
            [DllImport("libmysql.dll")]
            public static extern int mysql_real_escape_string(IntPtr A_0, byte[] A_1, byte[] A_2, int A_3);
            [DllImport("libmysql.dll")]
            public static extern int mysql_real_query(IntPtr A_0, byte[] A_1, int A_2);
            [DllImport("libmysql.dll")]
            public static extern int mysql_real_query(IntPtr A_0, string A_1, int A_2);
            [DllImport("libmysql.dll")]
            public static extern byte mysql_rollback(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern IntPtr mysql_row_tell(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern int mysql_select_db(IntPtr A_0, string A_1);
            [DllImport("libmysql.dll")]
            private static extern int mysql_send_long_data(IntPtr A_0, int A_1, byte[] A_2, long A_3);
            [DllImport("libmysql.dll")]
            public static extern int mysql_send_query(IntPtr A_0, byte[] A_1, int A_2);
            [DllImport("libmysql.dll")]
            private static extern IntPtr mysql_sqlstate(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern byte mysql_ssl_set(IntPtr A_0, string A_1, string A_2, string A_3, string A_4, string A_5);
            [DllImport("libmysql.dll")]
            public static extern IntPtr mysql_stat(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern long mysql_stmt_affected_rows(IntPtr A_0);
            [DllImport("libmysql.dll")]
            private static extern byte mysql_stmt_bind_param(IntPtr A_0, byte[] A_1);
            [DllImport("libmysql.dll")]
            private static extern byte mysql_stmt_bind_result(IntPtr A_0, byte[] A_1);
            [DllImport("libmysql.dll")]
            public static extern byte mysql_stmt_close(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern void mysql_stmt_data_seek(IntPtr A_0, long A_1);
            [DllImport("libmysql.dll")]
            public static extern int mysql_stmt_errno(IntPtr A_0);
            [DllImport("libmysql.dll")]
            private static extern IntPtr mysql_stmt_error(IntPtr A_0);
            [DllImport("libmysql.dll")]
            private static extern int mysql_stmt_execute(IntPtr A_0);
            [DllImport("libmysql.dll")]
            private static extern int mysql_stmt_fetch(IntPtr A_0);
            [DllImport("libmysql.dll")]
            private static extern int mysql_stmt_fetch_column(IntPtr A_0, byte[] A_1, int A_2, long A_3);
            [DllImport("libmysql.dll")]
            public static extern int mysql_stmt_field_count(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern byte mysql_stmt_free_result(IntPtr A_0);
            [DllImport("libmysql.dll")]
            private static extern IntPtr mysql_stmt_init(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern long mysql_stmt_num_rows(IntPtr A_0);
            [DllImport("libmysql.dll")]
            private static extern int mysql_stmt_param_count(IntPtr A_0);
            [DllImport("libmysql.dll")]
            private static extern IntPtr mysql_stmt_param_metadata(IntPtr A_0);
            [DllImport("libmysql.dll")]
            private static extern int mysql_stmt_prepare(IntPtr A_0, byte[] A_1, int A_2);
            [DllImport("libmysql.dll")]
            public static extern byte mysql_stmt_reset(IntPtr A_0);
            [DllImport("libmysql.dll")]
            private static extern IntPtr mysql_stmt_result_metadata(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern IntPtr mysql_stmt_row_seek(IntPtr A_0, IntPtr A_1);
            [DllImport("libmysql.dll")]
            public static extern IntPtr mysql_stmt_row_tell(IntPtr A_0);
            [DllImport("libmysql.dll")]
            private static extern int mysql_stmt_send_long_data(IntPtr A_0, int A_1, byte[] A_2, long A_3);
            [DllImport("libmysql.dll")]
            private static extern IntPtr mysql_stmt_sqlstate(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern int mysql_stmt_store_result(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern IntPtr mysql_store_result(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern int mysql_thread_id(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern int mysql_thread_safe();
            [DllImport("libmysql.dll")]
            public static extern IntPtr mysql_use_result(IntPtr A_0);
            [DllImport("libmysql.dll")]
            public static extern int mysql_warning_count(IntPtr A_0);
        }

        [SuppressUnmanagedCodeSecurity]
        internal class b
        {
            public static string a();
            public static string a(IntPtr A_0);
            public static IntPtr a(IntPtr A_0, bool A_1);
            public static byte a(IntPtr A_0, byte[] A_1, bool A_2);
            public static IntPtr a(IntPtr A_0, byte[] A_1, int A_2, bool A_3);
            public static int a(IntPtr A_0, byte[] A_1, int A_2, long A_3, bool A_4);
            public static int a(IntPtr A_0, int A_1, byte[] A_2, long A_3, bool A_4);
            public static string b();
            public static string b(IntPtr A_0);
            public static IntPtr b(IntPtr A_0, bool A_1);
            public static byte b(IntPtr A_0, byte[] A_1, bool A_2);
            public static string c(IntPtr A_0);
            public static int c(IntPtr A_0, bool A_1);
            public static string d(IntPtr A_0);
            public static int d(IntPtr A_0, bool A_1);
            public static string e(IntPtr A_0);
            public static int e(IntPtr A_0, bool A_1);
            public static string f(IntPtr A_0);
            public static string g(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern long mysql_affected_rows(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern byte mysql_autocommit(IntPtr A_0, int A_1);
            [DllImport("libmysqld.dll")]
            public static extern byte mysql_change_user(IntPtr A_0, string A_1, string A_2, string A_3);
            [DllImport("libmysqld.dll")]
            private static extern IntPtr mysql_character_set_name(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern void mysql_close(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern byte mysql_commit(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern void mysql_data_seek(IntPtr A_0, long A_1);
            [DllImport("libmysqld.dll")]
            public static extern void mysql_debug(string A_0);
            [DllImport("libmysqld.dll")]
            public static extern byte mysql_eof(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern int mysql_errno(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            private static extern IntPtr mysql_error(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern int mysql_escape_string(byte[] A_0, string A_1, int A_2);
            [DllImport("libmysqld.dll")]
            public static extern IntPtr mysql_fetch_field(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern IntPtr mysql_fetch_field_direct(IntPtr A_0, int A_1);
            [DllImport("libmysqld.dll")]
            public static extern IntPtr mysql_fetch_fields(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern IntPtr mysql_fetch_lengths(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern IntPtr mysql_fetch_row(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern int mysql_field_count(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern int mysql_field_seek(IntPtr A_0, int A_1);
            [DllImport("libmysqld.dll")]
            public static extern int mysql_field_tell(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern void mysql_free_result(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            private static extern IntPtr mysql_get_client_info();
            [DllImport("libmysqld.dll")]
            private static extern IntPtr mysql_get_host_info(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            private static extern IntPtr mysql_get_proto_info(IntPtr A_0);
            [DllImport("libmysqld.dll", EntryPoint="mysql_get_proto_info")]
            public static extern uint mysql_get_proto_info_411(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern IntPtr mysql_get_server_info(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            private static extern IntPtr mysql_info(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern IntPtr mysql_init(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern long mysql_insert_id(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern int mysql_kill(IntPtr A_0, int A_1);
            [DllImport("libmysqld.dll")]
            public static extern byte mysql_more_results(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern int mysql_next_result(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern int mysql_num_fields(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern long mysql_num_rows(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern int mysql_options(IntPtr A_0, ae A_1, IntPtr A_2);
            [DllImport("libmysqld.dll")]
            public static extern int mysql_options(IntPtr A_0, ae A_1, ref int A_2);
            [DllImport("libmysqld.dll")]
            public static extern IntPtr mysql_prepare(IntPtr A_0, string A_1, int A_2);
            [DllImport("libmysqld.dll")]
            public static extern int mysql_query(IntPtr A_0, string A_1);
            [DllImport("libmysqld.dll")]
            public static extern int mysql_query(IntPtr A_0, byte[] A_1);
            [DllImport("libmysqld.dll")]
            public static extern int mysql_read_query_result(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern IntPtr mysql_real_connect(IntPtr A_0, string A_1, string A_2, string A_3, string A_4, int A_5, string A_6, int A_7);
            [DllImport("libmysqld.dll")]
            public static extern int mysql_real_escape_string(IntPtr A_0, byte[] A_1, string A_2, int A_3);
            [DllImport("libmysqld.dll")]
            public static extern int mysql_real_escape_string(IntPtr A_0, byte[] A_1, byte[] A_2, int A_3);
            [DllImport("libmysqld.dll")]
            public static extern int mysql_real_query(IntPtr A_0, byte[] A_1, int A_2);
            [DllImport("libmysqld.dll")]
            public static extern int mysql_real_query(IntPtr A_0, string A_1, int A_2);
            [DllImport("libmysqld.dll")]
            public static extern byte mysql_rollback(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern IntPtr mysql_row_tell(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern int mysql_select_db(IntPtr A_0, string A_1);
            [DllImport("libmysqld.dll")]
            public static extern int mysql_send_query(IntPtr A_0, byte[] A_1, int A_2);
            [DllImport("libmysqld.dll")]
            internal static extern void mysql_server_end();
            [DllImport("libmysqld.dll")]
            internal static extern int mysql_server_init(int A_0, string[] A_1, string[] A_2);
            [DllImport("libmysqld.dll")]
            private static extern IntPtr mysql_sqlstate(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern byte mysql_ssl_set(IntPtr A_0, string A_1, string A_2, string A_3, string A_4, string A_5);
            [DllImport("libmysqld.dll")]
            private static extern IntPtr mysql_stat(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern long mysql_stmt_affected_rows(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            private static extern byte mysql_stmt_bind_param(IntPtr A_0, byte[] A_1);
            [DllImport("libmysqld.dll")]
            private static extern byte mysql_stmt_bind_result(IntPtr A_0, byte[] A_1);
            [DllImport("libmysqld.dll")]
            public static extern byte mysql_stmt_close(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern void mysql_stmt_data_seek(IntPtr A_0, long A_1);
            [DllImport("libmysqld.dll")]
            public static extern int mysql_stmt_errno(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern string mysql_stmt_error(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            private static extern int mysql_stmt_execute(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            private static extern int mysql_stmt_fetch(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            private static extern int mysql_stmt_fetch_column(IntPtr A_0, byte[] A_1, int A_2, long A_3);
            [DllImport("libmysqld.dll")]
            public static extern byte mysql_stmt_field_count(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern byte mysql_stmt_free_result(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            private static extern IntPtr mysql_stmt_init(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern long mysql_stmt_num_rows(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            private static extern int mysql_stmt_param_count(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            private static extern IntPtr mysql_stmt_param_metadata(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            private static extern int mysql_stmt_prepare(IntPtr A_0, byte[] A_1, int A_2);
            [DllImport("libmysqld.dll")]
            public static extern byte mysql_stmt_reset(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            private static extern IntPtr mysql_stmt_result_metadata(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern IntPtr mysql_stmt_row_seek(IntPtr A_0, IntPtr A_1);
            [DllImport("libmysqld.dll")]
            public static extern IntPtr mysql_stmt_row_tell(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            private static extern int mysql_stmt_send_long_data(IntPtr A_0, int A_1, byte[] A_2, long A_3);
            [DllImport("libmysqld.dll")]
            public static extern string mysql_stmt_sqlstate(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern int mysql_stmt_store_result(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern IntPtr mysql_store_result(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            internal static extern void mysql_thread_end();
            [DllImport("libmysqld.dll")]
            public static extern int mysql_thread_id(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            internal static extern int mysql_thread_init();
            [DllImport("libmysqld.dll")]
            public static extern int mysql_thread_safe();
            [DllImport("libmysqld.dll")]
            public static extern IntPtr mysql_use_result(IntPtr A_0);
            [DllImport("libmysqld.dll")]
            public static extern int mysql_warning_count(IntPtr A_0);
        }
    }
}

