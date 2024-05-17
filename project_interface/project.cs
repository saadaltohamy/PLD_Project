
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF         =  0, // (EOF)
        SYMBOL_ERROR       =  1, // (Error)
        SYMBOL_WHITESPACE  =  2, // Whitespace
        SYMBOL_MINUS       =  3, // '-'
        SYMBOL_MINUSMINUS  =  4, // '--'
        SYMBOL_EXCLAMEQ    =  5, // '!='
        SYMBOL_PERCENT     =  6, // '%'
        SYMBOL_LPAREN      =  7, // '('
        SYMBOL_RPAREN      =  8, // ')'
        SYMBOL_TIMES       =  9, // '*'
        SYMBOL_TIMESTIMES  = 10, // '**'
        SYMBOL_DIV         = 11, // '/'
        SYMBOL_COLON       = 12, // ':'
        SYMBOL_SEMI        = 13, // ';'
        SYMBOL_LBRACE      = 14, // '{'
        SYMBOL_RBRACE      = 15, // '}'
        SYMBOL_PLUS        = 16, // '+'
        SYMBOL_PLUSPLUS    = 17, // '++'
        SYMBOL_LT          = 18, // '<'
        SYMBOL_LTEQ        = 19, // '<='
        SYMBOL_EQ          = 20, // '='
        SYMBOL_EQEQ        = 21, // '=='
        SYMBOL_GT          = 22, // '>'
        SYMBOL_GTEQ        = 23, // '>='
        SYMBOL_CASE        = 24, // case
        SYMBOL_DIGIT       = 25, // Digit
        SYMBOL_DO          = 26, // do
        SYMBOL_DOUBLE      = 27, // double
        SYMBOL_ELSE        = 28, // else
        SYMBOL_END         = 29, // End
        SYMBOL_FLOAT       = 30, // float
        SYMBOL_FOR         = 31, // for
        SYMBOL_ID          = 32, // Id
        SYMBOL_IF          = 33, // if
        SYMBOL_INT         = 34, // int
        SYMBOL_START       = 35, // Start
        SYMBOL_STRING      = 36, // string
        SYMBOL_SWITCH      = 37, // switch
        SYMBOL_WHILE       = 38, // while
        SYMBOL_ASSIGN      = 39, // <assign>
        SYMBOL_CASE2       = 40, // <case>
        SYMBOL_CASE_LST    = 41, // <case_lst>
        SYMBOL_COND        = 42, // <cond>
        SYMBOL_DATA        = 43, // <data>
        SYMBOL_DIGIT2      = 44, // <digit>
        SYMBOL_DO_WHILE    = 45, // <do_while>
        SYMBOL_EXPRESSION  = 46, // <Expression>
        SYMBOL_FACTOR      = 47, // <factor>
        SYMBOL_FOR_STMT    = 48, // <for_stmt>
        SYMBOL_ID2         = 49, // <id>
        SYMBOL_IF_STMT     = 50, // <if_stmt>
        SYMBOL_MULEXP      = 51, // <MulExp>
        SYMBOL_OP          = 52, // <op>
        SYMBOL_PROGRAM     = 53, // <program>
        SYMBOL_STEP        = 54, // <step>
        SYMBOL_STMT        = 55, // <stmt>
        SYMBOL_STMT_LST    = 56, // <stmt_lst>
        SYMBOL_SWITCH_STMT = 57, // <switch_stmt>
        SYMBOL_VALUE       = 58, // <Value>
        SYMBOL_WHILE_STMT  = 59  // <while_stmt>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM_START_END                           =  0, // <program> ::= Start <stmt_lst> End
        RULE_STMT_LST                                    =  1, // <stmt_lst> ::= <stmt>
        RULE_STMT_LST2                                   =  2, // <stmt_lst> ::= <stmt> <stmt_lst>
        RULE_STMT                                        =  3, // <stmt> ::= <assign>
        RULE_STMT2                                       =  4, // <stmt> ::= <if_stmt>
        RULE_STMT3                                       =  5, // <stmt> ::= <switch_stmt>
        RULE_STMT4                                       =  6, // <stmt> ::= <for_stmt>
        RULE_STMT5                                       =  7, // <stmt> ::= <while_stmt>
        RULE_STMT6                                       =  8, // <stmt> ::= <do_while>
        RULE_ASSIGN_EQ_SEMI                              =  9, // <assign> ::= <id> '=' <Expression> ';'
        RULE_ID_ID                                       = 10, // <id> ::= Id
        RULE_EXPRESSION_PLUS                             = 11, // <Expression> ::= <Expression> '+' <MulExp>
        RULE_EXPRESSION_MINUS                            = 12, // <Expression> ::= <Expression> '-' <MulExp>
        RULE_EXPRESSION                                  = 13, // <Expression> ::= <MulExp>
        RULE_MULEXP_TIMES                                = 14, // <MulExp> ::= <MulExp> '*' <factor>
        RULE_MULEXP_DIV                                  = 15, // <MulExp> ::= <MulExp> '/' <factor>
        RULE_MULEXP_PERCENT                              = 16, // <MulExp> ::= <MulExp> '%' <factor>
        RULE_MULEXP                                      = 17, // <MulExp> ::= <factor>
        RULE_FACTOR_TIMESTIMES                           = 18, // <factor> ::= <factor> '**' <Value>
        RULE_FACTOR                                      = 19, // <factor> ::= <Value>
        RULE_VALUE_ID                                    = 20, // <Value> ::= Id
        RULE_VALUE                                       = 21, // <Value> ::= <digit>
        RULE_VALUE_LPAREN_RPAREN                         = 22, // <Value> ::= '(' <Expression> ')'
        RULE_DIGIT_DIGIT                                 = 23, // <digit> ::= Digit
        RULE_IF_STMT_IF_LBRACE_RBRACE                    = 24, // <if_stmt> ::= if <cond> '{' <stmt_lst> '}'
        RULE_IF_STMT_IF_LBRACE_RBRACE_ELSE_LBRACE_RBRACE = 25, // <if_stmt> ::= if <cond> '{' <stmt_lst> '}' else '{' <stmt_lst> '}'
        RULE_COND                                        = 26, // <cond> ::= <Expression> <op> <Expression>
        RULE_OP_LT                                       = 27, // <op> ::= '<'
        RULE_OP_GT                                       = 28, // <op> ::= '>'
        RULE_OP_EQEQ                                     = 29, // <op> ::= '=='
        RULE_OP_EXCLAMEQ                                 = 30, // <op> ::= '!='
        RULE_OP_GTEQ                                     = 31, // <op> ::= '>='
        RULE_OP_LTEQ                                     = 32, // <op> ::= '<='
        RULE_SWITCH_STMT_SWITCH_LBRACE_RBRACE            = 33, // <switch_stmt> ::= switch <id> '{' <case_lst> '}'
        RULE_CASE_LST                                    = 34, // <case_lst> ::= <case>
        RULE_CASE_LST2                                   = 35, // <case_lst> ::= <case> <case_lst>
        RULE_CASE_CASE_COLON                             = 36, // <case> ::= case <digit> ':' <stmt_lst>
        RULE_WHILE_STMT_WHILE_LBRACE_RBRACE              = 37, // <while_stmt> ::= while <cond> '{' <stmt_lst> '}'
        RULE_DO_WHILE_DO_WHILE_SEMI                      = 38, // <do_while> ::= do <stmt_lst> while <cond> ';'
        RULE_FOR_STMT_FOR_SEMI_SEMI_LBRACE_RBRACE        = 39, // <for_stmt> ::= for <data> <assign> ';' <cond> ';' <step> '{' <stmt_lst> '}'
        RULE_DATA_INT                                    = 40, // <data> ::= int
        RULE_DATA_FLOAT                                  = 41, // <data> ::= float
        RULE_DATA_DOUBLE                                 = 42, // <data> ::= double
        RULE_DATA_STRING                                 = 43, // <data> ::= string
        RULE_STEP_PLUSPLUS                               = 44, // <step> ::= <id> '++'
        RULE_STEP_PLUSPLUS2                              = 45, // <step> ::= '++' <id>
        RULE_STEP_MINUSMINUS                             = 46, // <step> ::= <id> '--'
        RULE_STEP_MINUSMINUS2                            = 47, // <step> ::= '--' <id>
        RULE_STEP                                        = 48  // <step> ::= <assign>
    };

    public class MyParser
    {
        private LALRParser parser;
        ListBox lst;
        ListBox tokenList;
        public MyParser(string filename, ListBox lst, ListBox lst2)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            this.lst = lst;
            this.tokenList = lst2;
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
            parser.OnTokenRead += new LALRParser.TokenReadHandler(TokenRedEvent);
        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSMINUS :
                //'--'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ :
                //'!='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PERCENT :
                //'%'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMESTIMES :
                //'**'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLON :
                //':'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SEMI :
                //';'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACE :
                //'{'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACE :
                //'}'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSPLUS :
                //'++'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LTEQ :
                //'<='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQEQ :
                //'=='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GTEQ :
                //'>='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE :
                //case
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT :
                //Digit
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DO :
                //do
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DOUBLE :
                //double
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_END :
                //End
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FLOAT :
                //float
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //for
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID :
                //Id
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INT :
                //int
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_START :
                //Start
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRING :
                //string
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH :
                //switch
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE :
                //while
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGN :
                //<assign>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE2 :
                //<case>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE_LST :
                //<case_lst>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COND :
                //<cond>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DATA :
                //<data>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT2 :
                //<digit>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DO_WHILE :
                //<do_while>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPRESSION :
                //<Expression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FACTOR :
                //<factor>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR_STMT :
                //<for_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID2 :
                //<id>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF_STMT :
                //<if_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MULEXP :
                //<MulExp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OP :
                //<op>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STEP :
                //<step>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STMT :
                //<stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STMT_LST :
                //<stmt_lst>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH_STMT :
                //<switch_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VALUE :
                //<Value>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE_STMT :
                //<while_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PROGRAM_START_END :
                //<program> ::= Start <stmt_lst> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_LST :
                //<stmt_lst> ::= <stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_LST2 :
                //<stmt_lst> ::= <stmt> <stmt_lst>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT :
                //<stmt> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT2 :
                //<stmt> ::= <if_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT3 :
                //<stmt> ::= <switch_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT4 :
                //<stmt> ::= <for_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT5 :
                //<stmt> ::= <while_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT6 :
                //<stmt> ::= <do_while>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGN_EQ_SEMI :
                //<assign> ::= <id> '=' <Expression> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ID_ID :
                //<id> ::= Id
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_PLUS :
                //<Expression> ::= <Expression> '+' <MulExp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_MINUS :
                //<Expression> ::= <Expression> '-' <MulExp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION :
                //<Expression> ::= <MulExp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULEXP_TIMES :
                //<MulExp> ::= <MulExp> '*' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULEXP_DIV :
                //<MulExp> ::= <MulExp> '/' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULEXP_PERCENT :
                //<MulExp> ::= <MulExp> '%' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULEXP :
                //<MulExp> ::= <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_TIMESTIMES :
                //<factor> ::= <factor> '**' <Value>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR :
                //<factor> ::= <Value>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_ID :
                //<Value> ::= Id
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE :
                //<Value> ::= <digit>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_LPAREN_RPAREN :
                //<Value> ::= '(' <Expression> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIGIT_DIGIT :
                //<digit> ::= Digit
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STMT_IF_LBRACE_RBRACE :
                //<if_stmt> ::= if <cond> '{' <stmt_lst> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STMT_IF_LBRACE_RBRACE_ELSE_LBRACE_RBRACE :
                //<if_stmt> ::= if <cond> '{' <stmt_lst> '}' else '{' <stmt_lst> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COND :
                //<cond> ::= <Expression> <op> <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_LT :
                //<op> ::= '<'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_GT :
                //<op> ::= '>'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EQEQ :
                //<op> ::= '=='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EXCLAMEQ :
                //<op> ::= '!='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_GTEQ :
                //<op> ::= '>='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_LTEQ :
                //<op> ::= '<='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SWITCH_STMT_SWITCH_LBRACE_RBRACE :
                //<switch_stmt> ::= switch <id> '{' <case_lst> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_LST :
                //<case_lst> ::= <case>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_LST2 :
                //<case_lst> ::= <case> <case_lst>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_CASE_COLON :
                //<case> ::= case <digit> ':' <stmt_lst>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_WHILE_STMT_WHILE_LBRACE_RBRACE :
                //<while_stmt> ::= while <cond> '{' <stmt_lst> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DO_WHILE_DO_WHILE_SEMI :
                //<do_while> ::= do <stmt_lst> while <cond> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_STMT_FOR_SEMI_SEMI_LBRACE_RBRACE :
                //<for_stmt> ::= for <data> <assign> ';' <cond> ';' <step> '{' <stmt_lst> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_INT :
                //<data> ::= int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_FLOAT :
                //<data> ::= float
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_DOUBLE :
                //<data> ::= double
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_STRING :
                //<data> ::= string
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_PLUSPLUS :
                //<step> ::= <id> '++'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_PLUSPLUS2 :
                //<step> ::= '++' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_MINUSMINUS :
                //<step> ::= <id> '--'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_MINUSMINUS2 :
                //<step> ::= '--' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP :
                //<step> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+" in line "+args.UnexpectedToken.Location.LineNr;
            string m2 = "Expected token: "+ args.ExpectedTokens.ToString()+"'";
            lst.Items.Add(message);
            lst.Items.Add(m2);
        }
        private void TokenRedEvent(LALRParser pr, TokenReadEventArgs args)
        {
            string message = args.Token.Text + "\t \t" + args.Token.Symbol.Id;
            tokenList.Items.Add(message);
        }

    }
}
