/* created on 07/02/2012 20:08:08 from peg generator V1.0 using 'UPnPContentDirectorySearch.g' as input*/

using Peg.Base;
using System;
using System.IO;
using System.Text;
namespace MediaPortal.Extensions.MediaServer
{
      
      public  enum EUPnPContentDirectorySearch{searchCrit= 1, parenthesized_expression= 2, primary_expression= 3, 
                                                existance_expression= 4, string_expression= 5, relational_expression= 6, 
                                                equality_expression= 7, conditional_and_expression= 8, conditional_or_expression= 9, 
                                                conditional_expression= 10, boolean_expression= 11, literal= 12, 
                                                string_literal= 13, boolean_literal= 14, string_literal_characters= 15, 
                                                string_literal_character= 16, property= 17, property_element= 18, 
                                                property_attribute= 19, regular_property_character= 20, wChar= 21, 
                                                B= 22, whitespace= 23, new_line= 24};
      public  class UPnPContentDirectorySearch : PegCharParser 
      {
        
         #region Input Properties
        public static EncodingClass encodingClass = EncodingClass.utf8;
        public static UnicodeDetection unicodeDetection = UnicodeDetection.notApplicable;
        #endregion Input Properties
        #region Constructors
        public UPnPContentDirectorySearch()
            : base()
        {
            
        }
        public UPnPContentDirectorySearch(string src,TextWriter FerrOut)
			: base(src,FerrOut)
        {
            
        }
        #endregion Constructors
        #region Overrides
        public override string GetRuleNameFromId(int id)
        {
            try
            {
                   EUPnPContentDirectorySearch ruleEnum = (EUPnPContentDirectorySearch)id;
                    string s= ruleEnum.ToString();
                    int val;
                    if( int.TryParse(s,out val) ){
                        return base.GetRuleNameFromId(id);
                    }else{
                        return s;
                    }
            }
            catch (Exception)
            {
                return base.GetRuleNameFromId(id);
            }
        }
        public override void GetProperties(out EncodingClass encoding, out UnicodeDetection detection)
        {
            encoding = encodingClass;
            detection = unicodeDetection;
        } 
        #endregion Overrides
		#region Grammar Rules
        public bool searchCrit()    /*^^searchCrit: boolean_expression / '*';*/
        {

           return TreeNT((int)EUPnPContentDirectorySearch.searchCrit,()=>
                    boolean_expression() || Char('*') );
		}
        public bool parenthesized_expression()    /*parenthesized_expression: '(' wChar boolean_expression ')' wChar;*/
        {

           return And(()=>  
                     Char('(')
                  && wChar()
                  && boolean_expression()
                  && Char(')')
                  && wChar() );
		}
        public bool primary_expression()    /*primary_expression:		literal / property / parenthesized_expression;*/
        {

           return     literal() || property() || parenthesized_expression();
		}
        public bool existance_expression()    /*^existance_expression:    primary_expression (^('exists') wChar primary_expression )*;*/
        {

           return TreeAST((int)EUPnPContentDirectorySearch.existance_expression,()=>
                And(()=>  
                     primary_expression()
                  && OptRepeat(()=>    
                      And(()=>      
                               TreeChars(()=> Char('e','x','i','s','t','s') )
                            && wChar()
                            && primary_expression() ) ) ) );
		}
        public bool string_expression()    /*^string_expression:    existance_expression (^('contains'/'doesNotContain'/'derivedfrom') wChar existance_expression )*;*/
        {

           return TreeAST((int)EUPnPContentDirectorySearch.string_expression,()=>
                And(()=>  
                     existance_expression()
                  && OptRepeat(()=>    
                      And(()=>      
                               TreeChars(()=>        
                                              
                                                 Char("contains")
                                              || Char("doesNotContain")
                                              || Char("derivedfrom") )
                            && wChar()
                            && existance_expression() ) ) ) );
		}
        public bool relational_expression()    /*^relational_expression: 	string_expression (^('<='/'>='/'<'/'>') wChar string_expression )*;*/
        {

           return TreeAST((int)EUPnPContentDirectorySearch.relational_expression,()=>
                And(()=>  
                     string_expression()
                  && OptRepeat(()=>    
                      And(()=>      
                               TreeChars(()=>        
                                              
                                                 Char('<','=')
                                              || Char('>','=')
                                              || Char('<')
                                              || Char('>') )
                            && wChar()
                            && string_expression() ) ) ) );
		}
        public bool equality_expression()    /*^equality_expression: 		relational_expression (^('='/'!=') wChar relational_expression)*;*/
        {

           return TreeAST((int)EUPnPContentDirectorySearch.equality_expression,()=>
                And(()=>  
                     relational_expression()
                  && OptRepeat(()=>    
                      And(()=>      
                               TreeChars(()=>     Char('=') || Char('!','=') )
                            && wChar()
                            && relational_expression() ) ) ) );
		}
        public bool conditional_and_expression()    /*^conditional_and_expression: 	equality_expression ('and' wChar equality_expression)*;*/
        {

           return TreeAST((int)EUPnPContentDirectorySearch.conditional_and_expression,()=>
                And(()=>  
                     equality_expression()
                  && OptRepeat(()=>    
                      And(()=>      
                               Char('a','n','d')
                            && wChar()
                            && equality_expression() ) ) ) );
		}
        public bool conditional_or_expression()    /*^conditional_or_expression: 	conditional_and_expression ('or' wChar conditional_and_expression)*;*/
        {

           return TreeAST((int)EUPnPContentDirectorySearch.conditional_or_expression,()=>
                And(()=>  
                     conditional_and_expression()
                  && OptRepeat(()=>    
                      And(()=>      
                               Char('o','r')
                            && wChar()
                            && conditional_and_expression() ) ) ) );
		}
        public bool conditional_expression()    /*^conditional_expression: conditional_or_expression;*/
        {

           return TreeAST((int)EUPnPContentDirectorySearch.conditional_expression,()=>
                conditional_or_expression() );
		}
        public bool boolean_expression()    /*^^boolean_expression: 		conditional_expression;*/
        {

           return TreeNT((int)EUPnPContentDirectorySearch.boolean_expression,()=>
                conditional_expression() );
		}
        public bool literal()    /*literal: (boolean_literal / string_literal) wChar;*/
        {

           return And(()=>  
                     (    boolean_literal() || string_literal())
                  && wChar() );
		}
        public bool string_literal()    /*^string_literal: '"' string_literal_characters? '"';*/
        {

           return TreeAST((int)EUPnPContentDirectorySearch.string_literal,()=>
                And(()=>  
                     Char('"')
                  && Option(()=> string_literal_characters() )
                  && Char('"') ) );
		}
        public bool boolean_literal()    /*^boolean_literal: 'true' B / 'false' B;*/
        {

           return TreeAST((int)EUPnPContentDirectorySearch.boolean_literal,()=>
                  
                     And(()=>    Char('t','r','u','e') && B() )
                  || And(()=>    Char('f','a','l','s','e') && B() ) );
		}
        public bool string_literal_characters()    /*string_literal_characters: string_literal_character+;*/
        {

           return PlusRepeat(()=> string_literal_character() );
		}
        public bool string_literal_character()    /*string_literal_character: [^"\\#x000D#x000A#x0085#x2028#x2029]; //Any character except " (U+0022), " (U+005C), and new-line-character*/
        {

           return NotOneOf("\"\\\u000d\u000a\u0085\u2028\u2029");
		}
        public bool property()    /*^^property: property_element property_attribute? wChar / property_attribute wChar;*/
        {

           return TreeNT((int)EUPnPContentDirectorySearch.property,()=>
                  
                     And(()=>    
                         property_element()
                      && Option(()=> property_attribute() )
                      && wChar() )
                  || And(()=>    property_attribute() && wChar() ) );
		}
        public bool property_element()    /*property_element: regular_property_character+ (':' regular_property_character*);*/
        {

           return And(()=>  
                     PlusRepeat(()=> regular_property_character() )
                  && And(()=>    
                         Char(':')
                      && OptRepeat(()=> regular_property_character() ) ) );
		}
        public bool property_attribute()    /*property_attribute: '@' regular_property_character+;*/
        {

           return And(()=>  
                     Char('@')
                  && PlusRepeat(()=> regular_property_character() ) );
		}
        public bool regular_property_character()    /*regular_property_character: [A-Za-z];*/
        {

           return In('A','Z', 'a','z');
		}
        public bool wChar()    /*wChar: ( whitespace / new_line )*;*/
        {

           return OptRepeat(()=>     whitespace() || new_line() );
		}
        public bool B()    /*B: ![a-zA-Z_0-9];*/
        {

           return Not(()=> (In('a','z', 'A','Z', '0','9')||OneOf("_")) );
		}
        public bool whitespace()    /*whitespace: #x0009/#x000B/#x000C/#x0020;   
// Horizontal tab character (U+0009)
// Vertical tab character (U+000B)
// Form feed character (U+000C)
// Space character (U+0020)*/
        {

           return   
                     Char('\u0009')
                  || Char('\u000b')
                  || Char('\u000c')
                  || Char('\u0020');
		}
        public bool new_line()    /*new_line: '\r\n' / [\r\n];
//	Carriage return character (U+000D) followed by line feed character (U+000A) /
//	Carriage return character (U+000D) /
//	Line feed character (U+000A) /*/
        {

           return     Char('\r','\n') || OneOf("\r\n");
		}
		#endregion Grammar Rules
   }
}