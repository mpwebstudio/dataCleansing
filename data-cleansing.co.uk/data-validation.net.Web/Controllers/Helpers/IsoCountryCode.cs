using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace data_validation.net.Web.Controllers.Helpers
{
    public class IsoCountryCode
    {

        private class Iso
        {
            public string Key { get; set; }

            public string Value { get; set; }
        }

        public static IEnumerable<SelectListItem> IsoCode()
        {

            var shosho = IsoCountryHelper().Select(x => new SelectListItem { Text = x.Key, Value = x.Value });

            return shosho;
        }

        private static List<Iso> IsoCountryHelper()
        {
            var country = new List<Iso>();
            country.Add(new Iso { Key = "Afghanistan", Value = "AF" });
            country.Add(new Iso { Key = "Aland Islands", Value = "AX" });
            country.Add(new Iso { Key = "Albania", Value = "AL" });
            country.Add(new Iso { Key = "Algeria", Value = "DZ" });
            country.Add(new Iso { Key = "American Samoa", Value = "AS" });
            country.Add(new Iso { Key = "Andorra", Value = "AD" });
            country.Add(new Iso { Key = "Angola", Value = "AO" });
            country.Add(new Iso { Key = "Anguilla", Value = "AI" });
            country.Add(new Iso { Key = "Antarctica", Value = "AQ" });
            country.Add(new Iso { Key = "Antigua and Barbuda", Value = "AG" });
            country.Add(new Iso { Key = "Argentina", Value = "AR" });
            country.Add(new Iso { Key = "Armenia", Value = "AM" });
            country.Add(new Iso { Key = "Aruba", Value = "AW" });
            country.Add(new Iso { Key = "Australia", Value = "AU" });
            country.Add(new Iso { Key = "Austria", Value = "AT" });
            country.Add(new Iso { Key = "Azerbaijan", Value = "AZ" });
            country.Add(new Iso { Key = "Bahamas", Value = "BS" });
            country.Add(new Iso { Key = "Bahrain", Value = "BH" });
            country.Add(new Iso { Key = "Bangladesh", Value = "BD" });
            country.Add(new Iso { Key = "Barbados", Value = "BB" });
            country.Add(new Iso { Key = "Belarus", Value = "BY" });
            country.Add(new Iso { Key = "Belgium", Value = "BE" });
            country.Add(new Iso { Key = "Belize", Value = "BZ" });
            country.Add(new Iso { Key = "Benin", Value = "BJ" });
            country.Add(new Iso { Key = "Bermuda", Value = "BM" });
            country.Add(new Iso { Key = "Bhutan", Value = "BT" });
            country.Add(new Iso { Key = "Bolivia", Value = "BO" });
            country.Add(new Iso { Key = "Bonaire, Sint Eustatius and Saba", Value = "BQ" });
            country.Add(new Iso { Key = "Bosnia and Herzegovina", Value = "BA" });
            country.Add(new Iso { Key = "Botswana", Value = "BW" });
            country.Add(new Iso { Key = "Bouvet Island", Value = "BV" });
            country.Add(new Iso { Key = "Brazil", Value = "BR" });
            country.Add(new Iso { Key = "British Indian Ocean Territory", Value = "IO" });
            country.Add(new Iso { Key = "Brunei Darussalam", Value = "BN" });
            country.Add(new Iso { Key = "Bulgaria", Value = "BG" });
            country.Add(new Iso { Key = "Burkina Faso", Value = "BF" });
            country.Add(new Iso { Key = "Burundi", Value = "BI" });
            country.Add(new Iso { Key = "Cabo Verde", Value = "CV" });
            country.Add(new Iso { Key = "Cambodia", Value = "KH" });
            country.Add(new Iso { Key = "Cameroon", Value = "CM" });
            country.Add(new Iso { Key = "Canada", Value = "CA" });
            country.Add(new Iso { Key = "Cayman Islands", Value = "KY" });
            country.Add(new Iso { Key = "Central African Republic", Value = "CF" });
            country.Add(new Iso { Key = "Chad", Value = "TD" });
            country.Add(new Iso { Key = "Chile", Value = "CL" });
            country.Add(new Iso { Key = "China", Value = "CN" });
            country.Add(new Iso { Key = "Christmas Island", Value = "CX" });
            country.Add(new Iso { Key = "Cocos (Keeling) Islands", Value = "CC" });
            country.Add(new Iso { Key = "Colombia", Value = "CO" });
            country.Add(new Iso { Key = "Comoros", Value = "KM" });
            country.Add(new Iso { Key = "Congo", Value = "CG" });
            country.Add(new Iso { Key = "Congo", Value = "CD" });
            country.Add(new Iso { Key = "Cook Islands", Value = "CK" });
            country.Add(new Iso { Key = "Costa Rica", Value = "CR" });
            country.Add(new Iso { Key = "Cote d'Ivoire", Value = "CI" });
            country.Add(new Iso { Key = "Croatia", Value = "HR" });
            country.Add(new Iso { Key = "Cuba", Value = "CU" });
            country.Add(new Iso { Key = "Curaçao", Value = "CW" });
            country.Add(new Iso { Key = "Cyprus", Value = "CY" });
            country.Add(new Iso { Key = "Czech Republic", Value = "CZ" });
            country.Add(new Iso { Key = "Denmark", Value = "DK" });
            country.Add(new Iso { Key = "Djibouti", Value = "DJ" });
            country.Add(new Iso { Key = "Dominica", Value = "DM" });
            country.Add(new Iso { Key = "Dominican Republic", Value = "DO" });
            country.Add(new Iso { Key = "Ecuador", Value = "EC" });
            country.Add(new Iso { Key = "Egypt", Value = "EG" });
            country.Add(new Iso { Key = "El Salvador", Value = "SV" });
            country.Add(new Iso { Key = "Equatorial Guinea", Value = "GQ" });
            country.Add(new Iso { Key = "Eritrea", Value = "ER" });
            country.Add(new Iso { Key = "Estonia", Value = "EE" });
            country.Add(new Iso { Key = "Ethiopia", Value = "ET" });
            country.Add(new Iso { Key = "Falkland Islands", Value = "FK" });
            country.Add(new Iso { Key = "Faroe Islands", Value = "FO" });
            country.Add(new Iso { Key = "Fiji", Value = "FJ" });
            country.Add(new Iso { Key = "Finland", Value = "FI" });
            country.Add(new Iso { Key = "France", Value = "FR" });
            country.Add(new Iso { Key = "French Guiana", Value = "GF" });
            country.Add(new Iso { Key = "French Polynesia", Value = "PF" });
            country.Add(new Iso { Key = "French Southern Territories", Value = "TF" });
            country.Add(new Iso { Key = "Gabon", Value = "GA" });
            country.Add(new Iso { Key = "Gambia", Value = "GM" });
            country.Add(new Iso { Key = "Georgia", Value = "GE" });
            country.Add(new Iso { Key = "Germany", Value = "DE" });
            country.Add(new Iso { Key = "Ghana", Value = "GH" });
            country.Add(new Iso { Key = "Gibraltar", Value = "GI" });
            country.Add(new Iso { Key = "Greece", Value = "GR" });
            country.Add(new Iso { Key = "Greenland", Value = "GL" });
            country.Add(new Iso { Key = "Grenada", Value = "GD" });
            country.Add(new Iso { Key = "Guadeloupe", Value = "GP" });
            country.Add(new Iso { Key = "Guam", Value = "GU" });
            country.Add(new Iso { Key = "Guatemala", Value = "GT" });
            country.Add(new Iso { Key = "Guernsey", Value = "GG" });
            country.Add(new Iso { Key = "Guinea", Value = "GN" });
            country.Add(new Iso { Key = "Guinea-Bissau", Value = "GW" });
            country.Add(new Iso { Key = "Guyana", Value = "GY" });
            country.Add(new Iso { Key = "Haiti", Value = "HT" });
            country.Add(new Iso { Key = "Heard Island and McDonald Islands", Value = "HM" });
            country.Add(new Iso { Key = "Holy See", Value = "VA" });
            country.Add(new Iso { Key = "Honduras", Value = "HN" });
            country.Add(new Iso { Key = "Hong Kong", Value = "HK" });
            country.Add(new Iso { Key = "Hungary", Value = "HU" });
            country.Add(new Iso { Key = "Iceland", Value = "IS" });
            country.Add(new Iso { Key = "India", Value = "IN" });
            country.Add(new Iso { Key = "Indonesia", Value = "ID" });
            country.Add(new Iso { Key = "Iran", Value = "IR" });
            country.Add(new Iso { Key = "Iraq", Value = "IQ" });
            country.Add(new Iso { Key = "Ireland", Value = "IE" });
            country.Add(new Iso { Key = "Isle of Man", Value = "IM" });
            country.Add(new Iso { Key = "Israel", Value = "IL" });
            country.Add(new Iso { Key = "Italy", Value = "IT" });
            country.Add(new Iso { Key = "Jamaica", Value = "JM" });
            country.Add(new Iso { Key = "Japan", Value = "JP" });
            country.Add(new Iso { Key = "Jersey", Value = "JE" });
            country.Add(new Iso { Key = "Jordan", Value = "JO" });
            country.Add(new Iso { Key = "Kazakhstan", Value = "KZ" });
            country.Add(new Iso { Key = "Kenya", Value = "KE" });
            country.Add(new Iso { Key = "Kiribati", Value = "KI" });
            country.Add(new Iso { Key = "Korea", Value = "KP" });
            country.Add(new Iso { Key = "Korea (Republic of)", Value = "KR" });
            country.Add(new Iso { Key = "Kuwait", Value = "KW" });
            country.Add(new Iso { Key = "Kyrgyzstan", Value = "KG" });
            country.Add(new Iso { Key = "Lao People's Democratic Republic", Value = "LA" });
            country.Add(new Iso { Key = "Latvia", Value = "LV" });
            country.Add(new Iso { Key = "Lebanon", Value = "LB" });
            country.Add(new Iso { Key = "Lesotho", Value = "LS" });
            country.Add(new Iso { Key = "Liberia", Value = "LR" });
            country.Add(new Iso { Key = "Libya", Value = "LY" });
            country.Add(new Iso { Key = "Liechtenstein", Value = "LI" });
            country.Add(new Iso { Key = "Lithuania", Value = "LT" });
            country.Add(new Iso { Key = "Luxembourg", Value = "LU" });
            country.Add(new Iso { Key = "Macao", Value = "MO" });
            country.Add(new Iso { Key = "Macedonia", Value = "MK" });
            country.Add(new Iso { Key = "Madagascar", Value = "MG" });
            country.Add(new Iso { Key = "Malawi", Value = "MW" });
            country.Add(new Iso { Key = "Malaysia", Value = "MY" });
            country.Add(new Iso { Key = "Maldives", Value = "MV" });
            country.Add(new Iso { Key = "Mali", Value = "ML" });
            country.Add(new Iso { Key = "Malta", Value = "MT" });
            country.Add(new Iso { Key = "Marshall Islands", Value = "MH" });
            country.Add(new Iso { Key = "Martinique", Value = "MQ" });
            country.Add(new Iso { Key = "Mauritania", Value = "MR" });
            country.Add(new Iso { Key = "Mauritius", Value = "MU" });
            country.Add(new Iso { Key = "Mayotte", Value = "YT" });
            country.Add(new Iso { Key = "Mexico", Value = "MX" });
            country.Add(new Iso { Key = "Micronesia", Value = "FM" });
            country.Add(new Iso { Key = "Moldova", Value = "MD" });
            country.Add(new Iso { Key = "Monaco", Value = "MC" });
            country.Add(new Iso { Key = "Mongolia", Value = "MN" });
            country.Add(new Iso { Key = "Montenegro", Value = "ME" });
            country.Add(new Iso { Key = "Montserrat", Value = "MS" });
            country.Add(new Iso { Key = "Morocco", Value = "MA" });
            country.Add(new Iso { Key = "Mozambique", Value = "MZ" });
            country.Add(new Iso { Key = "Myanmar", Value = "MM" });
            country.Add(new Iso { Key = "Namibia", Value = "NA" });
            country.Add(new Iso { Key = "Nauru", Value = "NR" });
            country.Add(new Iso { Key = "Nepal", Value = "NP" });
            country.Add(new Iso { Key = "Netherlands", Value = "NL" });
            country.Add(new Iso { Key = "New Caledonia", Value = "NC" });
            country.Add(new Iso { Key = "New Zealand", Value = "NZ" });
            country.Add(new Iso { Key = "Nicaragua", Value = "NI" });
            country.Add(new Iso { Key = "Niger", Value = "NE" });
            country.Add(new Iso { Key = "Nigeria", Value = "NG" });
            country.Add(new Iso { Key = "Niue", Value = "NU" });
            country.Add(new Iso { Key = "Norfolk Island", Value = "NF" });
            country.Add(new Iso { Key = "Northern Mariana Islands", Value = "MP" });
            country.Add(new Iso { Key = "Norway", Value = "NO" });
            country.Add(new Iso { Key = "Oman", Value = "OM" });
            country.Add(new Iso { Key = "Pakistan", Value = "PK" });
            country.Add(new Iso { Key = "Palau", Value = "PW" });
            country.Add(new Iso { Key = "Palestine, State of", Value = "PS" });
            country.Add(new Iso { Key = "Panama", Value = "PA" });
            country.Add(new Iso { Key = "Papua New Guinea", Value = "PG" });
            country.Add(new Iso { Key = "Paraguay", Value = "PY" });
            country.Add(new Iso { Key = "Peru", Value = "PE" });
            country.Add(new Iso { Key = "Philippines", Value = "PH" });
            country.Add(new Iso { Key = "Pitcairn", Value = "PN" });
            country.Add(new Iso { Key = "Poland", Value = "PL" });
            country.Add(new Iso { Key = "Portugal", Value = "PT" });
            country.Add(new Iso { Key = "Puerto Rico", Value = "PR" });
            country.Add(new Iso { Key = "Qatar", Value = "QA" });
            country.Add(new Iso { Key = "Reunion", Value = "RE" });
            country.Add(new Iso { Key = "Romania", Value = "RO" });
            country.Add(new Iso { Key = "Russian Federation", Value = "RU" });
            country.Add(new Iso { Key = "Rwanda", Value = "RW" });
            country.Add(new Iso { Key = "Saint Barthelemy", Value = "BL" });
            country.Add(new Iso { Key = "Saint Helena", Value = "SH" });
            country.Add(new Iso { Key = "Saint Kitts and Nevis", Value = "KN" });
            country.Add(new Iso { Key = "Saint Lucia", Value = "LC" });
            country.Add(new Iso { Key = "Saint Martin", Value = "MF" });
            country.Add(new Iso { Key = "Saint Pierre and Miquelon", Value = "PM" });
            country.Add(new Iso { Key = "Saint Vincent and the Grenadines", Value = "VC" });
            country.Add(new Iso { Key = "Samoa", Value = "WS" });
            country.Add(new Iso { Key = "San Marino", Value = "SM" });
            country.Add(new Iso { Key = "Sao Tome and Principe", Value = "ST" });
            country.Add(new Iso { Key = "Saudi Arabia", Value = "SA" });
            country.Add(new Iso { Key = "Senegal", Value = "SN" });
            country.Add(new Iso { Key = "Serbia", Value = "RS" });
            country.Add(new Iso { Key = "Seychelles", Value = "SC" });
            country.Add(new Iso { Key = "Sierra Leone", Value = "SL" });
            country.Add(new Iso { Key = "Singapore", Value = "SG" });
            country.Add(new Iso { Key = "Sint Maarten", Value = "SX" });
            country.Add(new Iso { Key = "Slovakia", Value = "SK" });
            country.Add(new Iso { Key = "Slovenia", Value = "SI" });
            country.Add(new Iso { Key = "Solomon Islands", Value = "SB" });
            country.Add(new Iso { Key = "Somalia", Value = "SO" });
            country.Add(new Iso { Key = "South Africa", Value = "ZA" });
            country.Add(new Iso { Key = "South Georgia", Value = "GS" });
            country.Add(new Iso { Key = "South Sudan", Value = "SS" });
            country.Add(new Iso { Key = "Spain", Value = "ES" });
            country.Add(new Iso { Key = "Sri Lanka", Value = "LK" });
            country.Add(new Iso { Key = "Sudan", Value = "SD" });
            country.Add(new Iso { Key = "Suriname", Value = "SR" });
            country.Add(new Iso { Key = "Svalbard and Jan Mayen", Value = "SJ" });
            country.Add(new Iso { Key = "Swaziland", Value = "SZ" });
            country.Add(new Iso { Key = "Sweden", Value = "SE" });
            country.Add(new Iso { Key = "Switzerland", Value = "CH" });
            country.Add(new Iso { Key = "Syrian Arab Republic", Value = "SY" });
            country.Add(new Iso { Key = "Taiwan", Value = "TW" });
            country.Add(new Iso { Key = "Tajikistan", Value = "TJ" });
            country.Add(new Iso { Key = "Tanzania", Value = "TZ" });
            country.Add(new Iso { Key = "Thailand", Value = "TH" });
            country.Add(new Iso { Key = "Timor-Leste", Value = "TL" });
            country.Add(new Iso { Key = "Togo", Value = "TG" });
            country.Add(new Iso { Key = "Tokelau", Value = "TK" });
            country.Add(new Iso { Key = "Tonga", Value = "TO" });
            country.Add(new Iso { Key = "Trinidad and Tobago", Value = "TT" });
            country.Add(new Iso { Key = "Tunisia", Value = "TN" });
            country.Add(new Iso { Key = "Turkey", Value = "TR" });
            country.Add(new Iso { Key = "Turkmenistan", Value = "TM" });
            country.Add(new Iso { Key = "Turks and Caicos Islands", Value = "TC" });
            country.Add(new Iso { Key = "Tuvalu", Value = "TV" });
            country.Add(new Iso { Key = "Uganda", Value = "UG" });
            country.Add(new Iso { Key = "Ukraine", Value = "UA" });
            country.Add(new Iso { Key = "United Arab Emirates", Value = "AE" });
            country.Add(new Iso { Key = "United Kingdom", Value = "GB" });
            country.Add(new Iso { Key = "United States of America", Value = "US" });
            country.Add(new Iso { Key = "Uruguay", Value = "UY" });
            country.Add(new Iso { Key = "Uzbekistan", Value = "UZ" });
            country.Add(new Iso { Key = "Vanuatu", Value = "VU" });
            country.Add(new Iso { Key = "Venezuela", Value = "VE" });
            country.Add(new Iso { Key = "Viet Nam", Value = "VN" });
            country.Add(new Iso { Key = "Virgin Islands (British)", Value = "VG" });
            country.Add(new Iso { Key = "Virgin Islands (U.S.)", Value = "VI" });
            country.Add(new Iso { Key = "Wallis and Futuna", Value = "WF" });
            country.Add(new Iso { Key = "Western Sahara", Value = "EH" });
            country.Add(new Iso { Key = "Yemen", Value = "YE" });
            country.Add(new Iso { Key = "Zambia", Value = "ZM" });
            country.Add(new Iso { Key = "Zimbabwe", Value = "ZW" });

            return country;
        }

    }

    public enum ISOCountryCode
    {
        AF,
        AX,
        AL,
        DZ,
        AS,
        AD,
        AO,
        AI,
        AQ,
        AG,
        AR,
        AM,
        AW,
        AU,
        AT,
        AZ,
        BS,
        BH,
        BD,
        BB,
        BY,
        BE,
        BZ,
        BJ,
        BM,
        BT,
        BO,
        BQ,
        BA,
        BW,
        BV,
        BR,
        IO,
        BN,
        BG,
        BF,
        BI,
        KH,
        CM,
        CA,
        CV,
        KY,
        CF,
        TD,
        CL,
        CN,
        CX,
        CC,
        CO,
        KM,
        CG,
        CD,
        CK,
        CR,
        CI,
        HR,
        CU,
        CW,
        CY,
        CZ,
        DK,
        DJ,
        DM,
        DO,
        EC,
        EG,
        SV,
        GQ,
        ER,
        EE,
        ET,
        FK,
        FO,
        FJ,
        FI,
        FR,
        GF,
        PF,
        TF,
        GA,
        GM,
        GE,
        DE,
        GH,
        GI,
        GR,
        GL,
        GD,
        GP,
        GU,
        GT,
        GG,
        GN,
        GW,
        GY,
        HT,
        HM,
        VA,
        HN,
        HK,
        HU,
        IS,
        IN,
        ID,
        IR,
        IQ,
        IE,
        IM,
        IL,
        IT,
        JM,
        JP,
        JE,
        JO,
        KZ,
        KE,
        KI,
        KP,
        KR,
        KW,
        KG,
        LA,
        LV,
        LB,
        LS,
        LR,
        LY,
        LI,
        LT,
        LU,
        MO,
        MK,
        MG,
        MW,
        MY,
        MV,
        ML,
        MT,
        MH,
        MQ,
        MR,
        MU,
        YT,
        MX,
        FM,
        MD,
        MC,
        MN,
        ME,
        MS,
        MA,
        MZ,
        MM,
        NA,
        NR,
        NP,
        NL,
        NC,
        NZ,
        NI,
        NE,
        NG,
        NU,
        NF,
        MP,
        NO,
        OM,
        PK,
        PW,
        PS,
        PA,
        PG,
        PY,
        PE,
        PH,
        PN,
        PL,
        PT,
        PR,
        QA,
        RE,
        RO,
        RU,
        RW,
        BL,
        SH,
        KN,
        LC,
        MF,
        PM,
        VC,
        WS,
        SM,
        ST,
        SA,
        SN,
        RS,
        SC,
        SL,
        SG,
        SX,
        SK,
        SI,
        SB,
        SO,
        ZA,
        GS,
        SS,
        ES,
        LK,
        SD,
        SR,
        SJ,
        SZ,
        SE,
        CH,
        SY,
        TW,
        TJ,
        TZ,
        TH,
        TL,
        TG,
        TK,
        TO,
        TT,
        TN,
        TR,
        TM,
        TC,
        TV,
        UG,
        UA,
        AE,
        GB,
        US,
        UM,
        UY,
        UZ,
        VU,
        VE,
        VN,
        VG,
        VI,
        WF,
        EH,
        YE,
        ZM,
        ZW,
    }
}