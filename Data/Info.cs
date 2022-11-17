using System.Collections.Generic;

namespace works.Data
{
    public class Info
    {
        public static Dictionary<string, string> Conf = new Dictionary<string, string>()
        {
            { "DB", "" },
            { "COMM", "" },
            { "FTP", "" },
        };

        // 5digit >  1~2:page, 3:성공(0),1(실패), 4~5:description
        public static Dictionary<string, string[]> Errors = new Dictionary<string, string[]>()
        {
            { "02001", new string[2] { "Success", "변경하였습니다." } },
            { "02002", new string[2] { "Success", "비밀번호 변경하였습니다." } },
            { "02101", new string[2] { "Warning Password", "기존&신규 비밀번호를 입력해주세요." } },
            { "02102", new string[2] { "Warning Password", "기존 비밀번호가 틀립니다." } },
            { "02103", new string[2] { "Warning Password", "변경 실패하였습니다." } },            
        };        
    }
}