Imports Newtonsoft.Json.JsonConvert



Namespace GRAMMARS
    ''' <summary>
    ''' Used as For seaperate Grammar Projects (Natural Language interface)
    ''' </summary>
    Public Class EnglishLanguageGrammar


        Public Shared Function AddEOF() As GrammarRule
            Dim Rule = New GrammarRule
            Rule.TAGSTRING = "END OF FILE"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("_EOF")

            Return Rule
        End Function
#Region "SIMPLE CONSTITUANT GRAMMAR"

        ''' <SUMMARY>
        ''' CREATES BASIC GRAMMAR WITH CONSTITUANTS
        '''NOUN = N
        '''VERB = V
        '''PREPOSITION = P
        '''DETERMINER = DET
        '''ADVERB = ADV
        '''ADJECTIVE = ADJ
        '''QUANTIFIER = QUANT
        '''QUESTWORD = QUEST
        '''AUXILLARY VERB = VAV
        '''PRONOUN = PRN
        '''PRONOUN_NAME = PNN
        '''PRONOUN_PLACE = PNP
        '''INTRANSITIVE VERB = VITV
        '''TRANSITIVE VERB = VTV
        '''INTERJECTION = INTERJ
        '''CONJUNCTION = CONJ
        '''NUMBER = NUM
        ''' </SUMMARY>
        ''' <RETURNS></RETURNS>
        Public Shared Function SIMPLECONSTITUANTGRAMMAR() As List(Of GrammarRule)
            SIMPLECONSTITUANTGRAMMAR = New List(Of GrammarRule) From {
                ADDNUMBER(),
                ADDCONJUNCTION(),
                ADDINTERJECTION(),
                ADDNOUN(),
                ADDVERB(),
                ADDPREPOSITION(),
                ADDDETERMINER(),
                ADDADVERB(),
                ADDADJECTIVE(),
                ADDQUANTIFIER(),
                ADDQUESTWORD(),
                ADDAUXVERB(),
                ADDPRONOUN(),
                ADDPRONOUNNAME(),
                ADDPRONOUNPLACE(),
                ADDTRANSITIVEVERB(),
                ADDINTRANSITIVEVERB(),
                AddNewline(),
                AddWhiteSpce()
            }
        End Function
        Public Shared Function AddNewline() As GrammarRule
            Dim Rule = New GrammarRule
            Rule.TAGSTRING = "_NEW_LINE"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add(ControlChars.CrLf)
            '   Rule.COMPONENTSTRINGS.Add(".")
            Return Rule
        End Function
        Public Shared Function AddWhiteSpce() As GrammarRule
            Dim Rule = New GrammarRule
            Rule.TAGSTRING = "_WHITE_SPACE"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add(" ")
            Return Rule
        End Function
        Public Shared Function ADDNUMBER() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "_NUMBER"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$NUM$")
            ADDNUMBER = NEWGRAMMARRULE
        End Function
        Public Shared Function ADDCONJUNCTION() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "_CONJUNCTION"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$CONJ$")
            ADDCONJUNCTION = NEWGRAMMARRULE
        End Function
        Public Shared Function ADDINTERJECTION() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "_INTERJECTION"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$INTERJ$")
            ADDINTERJECTION = NEWGRAMMARRULE
        End Function
        Public Shared Function ADDNOUN() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "_NOUN"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$N$")
            ADDNOUN = NEWGRAMMARRULE
        End Function
        Public Shared Function ADDVERB() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "_VERB"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$V$")
            ADDVERB = NEWGRAMMARRULE
        End Function
        Public Shared Function ADDPREPOSITION() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "_PREPOSITION"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$P$")
            ADDPREPOSITION = NEWGRAMMARRULE
        End Function
        Public Shared Function ADDADVERB() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "_ADVERB"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$ADV$")
            ADDADVERB = NEWGRAMMARRULE
        End Function
        Public Shared Function ADDDETERMINER() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "_DETERMINER"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$DET$")
            ADDDETERMINER = NEWGRAMMARRULE
        End Function
        Public Shared Function ADDADJECTIVE() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "_ADJECTIVE"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$ADJ$")
            ADDADJECTIVE = NEWGRAMMARRULE
        End Function
        Public Shared Function ADDQUANTIFIER() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule

            NEWGRAMMARRULE.COMPONENTSTRINGS = New List(Of String)
            NEWGRAMMARRULE.TAGSTRING = "_QUANTIFIER"
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUANT$")

            ADDQUANTIFIER = NEWGRAMMARRULE
        End Function
        Public Shared Function ADDQUESTWORD() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "_QUESTWORD"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUEST$")
            ADDQUESTWORD = NEWGRAMMARRULE
        End Function
        Public Shared Function ADDPRONOUN() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "_PRONOUN"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$PRN$")
            ADDPRONOUN = NEWGRAMMARRULE
        End Function
        Public Shared Function ADDAUXVERB() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "_AUX_VERB"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$VAV$")
            ADDAUXVERB = NEWGRAMMARRULE
        End Function
        Public Shared Function ADDPRONOUNPLACE() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "_PRONUN_PLACE"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$PNP$")
            ADDPRONOUNPLACE = NEWGRAMMARRULE
        End Function
        Public Shared Function ADDPRONOUNNAME() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "_PRONNOUN_NAME"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$PNN$")
            ADDPRONOUNNAME = NEWGRAMMARRULE
        End Function
        Public Shared Function ADDTRANSITIVEVERB() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "_TRANSITIVE_VERB"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$VTV$")
            ADDTRANSITIVEVERB = NEWGRAMMARRULE
        End Function
        Public Shared Function ADDINTRANSITIVEVERB() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "_INTRANSITIVE_vERB"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$VITV$")
            ADDINTRANSITIVEVERB = NEWGRAMMARRULE
        End Function

#End Region
#Region "SIMPLE PHRASE GRAMMAR"



        ''' <SUMMARY>
        ''' EACH PHRASETYPE DOES NOT CONTAIN SUB PHRASETYPES COMPLEX/COMPOUND  NP = P NP
        ''' WHICH HAVE BEEN DISABLED IN THIS GRAMMAR
        ''' PHRASES ALSO DO NOT CONTAIN SINGLE CONSTITUANTS IE NP = N
        '''
        ''' DETERMINATE PHRASE = DETP
        ''' ADVERB PHRASE = ADVP
        ''' ADJECTIVE PHRASE = ADJP
        ''' NOUN PHRASE = NP
        ''' VERB PHRASE = VP
        ''' PREPOSITIONAL PHRASE = PP
        ''' </SUMMARY>
        ''' <RETURNS></RETURNS>
        Public Shared Function SIMPLEPHRASEGRAMMAR() As List(Of GrammarRule)
            SIMPLEPHRASEGRAMMAR = New List(Of GrammarRule)
            SIMPLEPHRASEGRAMMAR.Add(ADDDETERMINATEPHRASES)
            SIMPLEPHRASEGRAMMAR.Add(ADDADJECTIVEPHRASES)
            SIMPLEPHRASEGRAMMAR.Add(ADDADVERBPHRASES)
            SIMPLEPHRASEGRAMMAR.Add(ADDNOUNPHRASES)
            SIMPLEPHRASEGRAMMAR.Add(ADDVERBPHRASES)
            SIMPLEPHRASEGRAMMAR.Add(ADDPREPOSITIONALPHRASES)

        End Function

        Public Shared Function ADDDETERMINATEPHRASES() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$DETP$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$ADV$ $QUANT$ $P$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$ADV$ $QUANT $P$ $DET$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$ADV$ $NUM$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$ADV$ $NUM$ $P$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$ADV$ $NUM$ $P$ $DET$")
            '    NEWGRAMMARRULE.COMPONENTSTRINGS.ADD("$DET$")
            ADDDETERMINATEPHRASES = NEWGRAMMARRULE
        End Function

        Public Shared Function ADDNOUNPHRASES() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$NP$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$ADJ$ $N$")
            'NEWGRAMMARRULE.COMPONENTSTRINGS.ADD("$DETP$ $N$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$DET$ $N$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$DET$ $ADJ$ $N$")
            'NEWGRAMMARRULE.COMPONENTSTRINGS.ADD("$DET$ $N$ PP$")
            'NEWGRAMMARRULE.COMPONENTSTRINGS.ADD("$DET$ $ADJ$ $N$ $PP$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$PRN$")
            'NEWGRAMMARRULE.COMPONENTSTRINGS.ADD("$N$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$PNN$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$PNP$")
            ADDNOUNPHRASES = NEWGRAMMARRULE
        End Function

        Public Shared Function ADDADVERBPHRASES() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$ADVP$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$ADV$ ADV$")
            ' NEWGRAMMARRULE.COMPONENTSTRINGS.ADD("$ADV$ $PP$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$ADV$")
            ADDADVERBPHRASES = NEWGRAMMARRULE
        End Function

        Public Shared Function ADDADJECTIVEPHRASES() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$ADJP$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$ADV$ $ADJ$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$ADJ$ ADV")
            'NEWGRAMMARRULE.COMPONENTSTRINGS.ADD("$NP$ $ADJ$")
            'NEWGRAMMARRULE.COMPONENTSTRINGS.ADD("$ADJ$")
            ADDADJECTIVEPHRASES = NEWGRAMMARRULE
        End Function

        Public Shared Function ADDPREPOSITIONALPHRASES() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$PP$"
            }
            'NEWGRAMMARRULE.COMPONENTSTRINGS.ADD("$ADV$ $P$ $NP$")
            'NEWGRAMMARRULE.COMPONENTSTRINGS.ADD("$P$ $NP$")
            'NEWGRAMMARRULE.COMPONENTSTRINGS.ADD("$P$")
            ADDPREPOSITIONALPHRASES = NEWGRAMMARRULE
        End Function

        Public Shared Function ADDVERBPHRASES() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$VP$"
            }
            ' NEWGRAMMARRULE.COMPONENTSTRINGS.ADD("$V PP")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$V$ $P$")
            ' NEWGRAMMARRULE.COMPONENTSTRINGS.ADD("$V$")
            ' NEWGRAMMARRULE.COMPONENTSTRINGS.ADD("$V$ $NP")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$VAV$ $V$")
            ADDVERBPHRASES = NEWGRAMMARRULE
        End Function

#End Region
#Region "COMPLEX PHRASE GRAMMAR"

        ''' <SUMMARY>
        ''' THIS GRAMMAR EXTENDS THE SIMPLE PHRASE GRAMMAR;
        ''' THIS GRAMMAR ADDS THE SUB PHRASE PATERNS OF SPEECH
        ''' IE: VP = V NP
        ''' </SUMMARY>
        ''' <PARAM NAME="CUSTOMRULES">REQUIRES SIMPLE PHRASE GRAMMAR </PARAM>
        ''' <RETURNS>GRAMMAR WITH EXTENDED COMPLEXITY</RETURNS>
        Public Shared Function COMPLEXPHRASEGRAMMAR() As List(Of GrammarRule)
            Dim CUSTOMRULES As New List(Of GrammarRule)
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$VP$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$V PP")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$V$ $NP")
            CUSTOMRULES.Add(NEWGRAMMARRULE)

            NEWGRAMMARRULE = New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$PP$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$ADV$ $P$ $NP$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$P$ $NP$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$P$")
            CUSTOMRULES.Add(NEWGRAMMARRULE)

            NEWGRAMMARRULE = New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$ADJP$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$NP$ $ADJ$")
            CUSTOMRULES.Add(NEWGRAMMARRULE)
            NEWGRAMMARRULE = New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$ADV$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$ADV$ $PP$")
            CUSTOMRULES.Add(NEWGRAMMARRULE)

            NEWGRAMMARRULE = New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$NP$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$DETP$ $N$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$DET$ $N$ PP$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$DET$ $ADJ$ $N$ $PP$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$N$")
            CUSTOMRULES.Add(NEWGRAMMARRULE)

            Return CUSTOMRULES
        End Function

#End Region
#Region "SIMPLESENTENCEGRAMMAR"


        ''' <SUMMARY>
        '''----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        '''SENTENCE STRUCTURES :
        '''----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        '''SENTENCES CONTAIN CLAUSES: CLAUSES CAN BE JOINED BY CONJUNCTIONS SUCH AS (SUBORDINATE CONJUNCTION) OR (COORDINATING CONJUNCTION) OR (RELATIVE PRONOUN)
        '''THE (SUBORDINATE CONJUNCTION) HELPS THE TRANSITION BETWEEN TWO PARTS OF A SENTENCE WITH WORDS,
        '''EXPRESSING THINGS LIKE PLACE AND TIME. THE (RELATIVE PRONOUN) INTRODUCE A DEPENDENT CLAUSE.
        '''THEY ARE CALLED “RELATIVE” BECAUSE THEY ARE RELATED TO THE TOPIC OF THE SENTENCE.
        '''THE (COORDINATING CONJUNCTION) JOINS TWO CLAUSES TOGETHER TO FORM A SINGLE CLAUSE
        '''----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        '''THE (SUBORDINATE CONJUNCTION)= AFTER,AS,AS LONG AS,ALTHOUGH,BECAUSE,BEFORE,EVEN IF,EVEN THOUGH,IF,NOW,NOW THAT,ONCE,SINCE,THAN,THOUGH,UNLESS,UNTIL,WHEN,WHENEVER,WHEREAS,WHEREVER,WHETHER,WHILE,WHOEVER
        '''THE (RELATIVE PRONOUN)= WHICH, WHICHEVER, WHATEVER, THAT, WHO, WHOEVER, AND WHOSE.
        '''THE (COORDINATING CONJUNCTION)= AND, BUT, YET, OR, NOR, FOR, SO
        ''' </SUMMARY>
        ''' <RETURNS></RETURNS>
        Public Shared Function SIMPLESENTENCEGRAMMAR() As List(Of GrammarRule)
            SIMPLESENTENCEGRAMMAR = New List(Of GrammarRule)
            SIMPLESENTENCEGRAMMAR.Add(ADDSIMPLE)
            SIMPLESENTENCEGRAMMAR.Add(ADDCOMPOUND)
            SIMPLESENTENCEGRAMMAR.Add(ADDCOMPLEX)
        End Function

        ''' <SUMMARY>
        '''SIMPLE SENTENCE
        '''A SIMPLE SENTENCE CONTAINS A SUBJECT AND A VERB,
        '''AND IT MAY ALSO HAVE AN OBJECT AND MODIFIERS.
        '''HOWEVER, IT CONTAINS ONLY ONE INDEPENDENT CLAUSE.
        '''SIMPLE SENTENCE = CLAUSE =	INDEPENDANT CLAUSE
        ''' </SUMMARY>
        ''' <RETURNS></RETURNS>
        Public Shared Function ADDSIMPLE() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$SIMPLE$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$CLAUSE$")
            ADDSIMPLE = NEWGRAMMARRULE
        End Function

        ''' <SUMMARY>
        '''COMPOUND SENTENCE
        '''A COMPOUND SENTENCE CONTAINS AT LEAST TWO INDEPENDENT CLAUSES.
        '''THESE TWO INDEPENDENT CLAUSES CAN BE COMBINED WITH A COMMA AND
        '''A COORDINATING CONJUNCTION OR WITH A SEMICOLON.
        '''COMPOUND SENTENCE  = SIMPLE SENTENCE CONJ SIMPLE SENTENCE =INDEPENDANT CLAUSE-SUBORDINATE CONJUNCTION-INDEPENDANT CLAUSE
        ''' </SUMMARY>
        ''' <RETURNS></RETURNS>
        Public Shared Function ADDCOMPOUND() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$COMPOUND$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$SIMPLE$ $CONJ$ $SIMPLE$")
            ADDCOMPOUND = NEWGRAMMARRULE
        End Function

        ''' <SUMMARY>
        '''COMPLEX SENTENCE
        '''A COMPLEX SENTENCE CONTAINS AT LEAST ONE INDEPENDENT CLAUSE AND AT LEAST ONE DEPENDENT CLAUSE.
        '''DEPENDENT CLAUSES 
        '''CAN REFER TO THE SUBJECT
        '''(WHO, WHICH) THE SEQUENCE/TIME (SINCE, WHILE),
        '''OR THE CAUSAL ELEMENTS (BECAUSE, IF) OF THE INDEPENDENT CLAUSE
        '''COMPLEX SENTENCE =	COMPOUND SENTENCE,SUBORDINATE CONJUNCTION,COMPOUND SENTENCE> =	INDEPENDANT CLAUSE ,SUBORDINATE CONJUNCTION,DEPENDANT CLAUSE
        '''COMPLEX SENTENCE =	SIMPLE SENTENCE,SUBORDINATE CONJUNCTION,SIMPLE SENTENCE> =		INDEPENDANT CLAUSE,SUBORDINATE CONJUNCTION,DEPENDANT CLAUSE
        '''COMPLEX SENTENCE =	COMPOUND SENTENCE,SUBORDINATE CONJUNCTION,SIMPLE SENTENCE> =	INDEPENDANT CLAUSE,SUBORDINATE CONJUNCTION,DEPENDANT CLAUSE
        '''COMPLEX SENTENCE =	COMPLEX SENTENCE,SUBORDINATE CONJUNCTION,COMPLEX SENTENCE> =	INDEPENDANT CLAUSE,SUBORDINATE CONJUNCTION,DEPENDANT CLAUSE
        ''' </SUMMARY>
        ''' <RETURNS></RETURNS>
        Public Shared Function ADDCOMPLEX() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$COMPLEX$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$SIMPLE$ $SUBCONJ$ $SIMPLE$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$COMPLEX$ $SUBCONJ$ $COMPLEX$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$COMPOUND$ $SUBCONJ$ $COMPOUND$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$SIMPLE$ $SUBCONJ$ $COMPOUND$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$COMPLEX$ $SUBCONJ$ $COMPOUND$")
            ADDCOMPLEX = NEWGRAMMARRULE
        End Function

#End Region
#Region "ComplexSentenceGrammar"

        ''' <SUMMARY>
        '''----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        ''' COMPLEXSENTENCEGRAMMAR ADDS SENTENCE TYPES :  DECLARITIVE/IMPERITIVE/INTERROGATIVE/EXCLAMITORY
        ''' CONSTITIUANTS : SUBJECT/PREDICATE
        '''GIVES TO THE PURPOSE OF THE SENTENCE
        '''----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        ''' </SUMMARY>
        ''' <RETURNS></RETURNS>
        Public Shared Function COMPLEXSENTENCEGRAMMAR() As List(Of GrammarRule)
            COMPLEXSENTENCEGRAMMAR = New List(Of GrammarRule) From {
                SUBJECT(),
                PREDICATE(),
                DECLARITIVE(),
                IMPERATIVE(),
                INTERROGATIVE(),
                EXCLAMITORY(),
                DECLARITIVE_CONCEPT(),
                INTERROGATIVE_CONCEPT()
            }

            Return COMPLEXSENTENCEGRAMMAR
        End Function

        ''' <SUMMARY>
        '''IMPERITIVE
        '''PURPOSES: TO GIVE COMMANDS, WARNINGS, SUGGESTIONS, OR ADVICE, TO REQUEST SOMETHING
        '''PUNCTUATION ".", "!"
        '''THE WORD "IMPERATIVE" GIVE COMMANDS, AND IMPERATIVE SENTENCES ARE COMMANDS.
        '''THE IMPLIED SUBJECT OF AN IMPERATIVE SENTENCE IS "YOU".
        '''IMPERATIVE SENTENCES ARE GENERALLY TERMINATED WITH AN EXCLAMATION MARK INSTEAD OF A PERIOD.
        '''IMPERATIVE SENTENCE= PREDICATE>= VERB>  COMPLEMENT>= SPEND THE MONEY! / GO TO YOUR ROOM!
        '''IMPERITIVE
        '''CLAUSE = SUBJECT (NP) PREDICATE = PRED(VP) (PUNCT = ! OR .)				CLAUSE >! OR .>
        ''' </SUMMARY>
        ''' <RETURNS></RETURNS>
        Public Shared Function IMPERATIVE() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$IMPERATIVE$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$PREDICATE$ .")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$PREDICATE$ .")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$PREDICATE$.")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$PREDICATE$!")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$PREDICATE$ !")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$V$ COMP$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$V$ COMP$.")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$V$ COMP$ .")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$V$ COMP$!")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$V$ COMP$ !")
            IMPERATIVE = NEWGRAMMARRULE
        End Function

        ''' <SUMMARY>
        '''DECLARITIVE
        '''PURPOSE: TO MAKE A STATEMENT
        '''PUNCTUATION "."
        '''DECLARATIVE SENTENCES ARE USED TO FORM STATEMENTS.
        '''DECLARATIVE SENTENCE>= SUBJECT> PREDICATE> .
        ''' </SUMMARY>
        ''' <RETURNS></RETURNS>
        Public Shared Function DECLARITIVE() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$DECLARITIVE$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$SUBJECT$ $PREDICATE$ $PUNCT$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$SUBJECT$ $PREDICATE$ .")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$SUBJECT$ $PREDICATE$.")

            DECLARITIVE = NEWGRAMMARRULE
        End Function

        ''' <SUMMARY>
        '''DECLARITIVE
        '''PURPOSE: TO MAKE A STATEMENT based on Concepts paterns Stored (LinkingVerb)
        ''' </SUMMARY>
        ''' <RETURNS></RETURNS>
        Public Shared Function DECLARITIVE_CONCEPT() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$DECLARITIVE_CONCEPT$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS = New List(Of String) From {
                "$NP$ $CONCEPT$ $NP$.",
                "$SUBJECT$ $CONCEPT$ $SUBJECT$."
            }
            DECLARITIVE_CONCEPT = NEWGRAMMARRULE
        End Function

        ''' <summary>
        ''' enables for recogognition of Intergative Sentences
        ''' for retriving informaion stored in concepts
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function INTERROGATIVE_CONCEPT() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$INTERROGATIVE_CONCEPT$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS = New List(Of String) From {
                "$QUESTWORD$ $SUBJECT$ $CONCEPT$ ?",
                "$QUESTWORD$ $SUBJECT$ $CONCEPT$?"
            }
            INTERROGATIVE_CONCEPT = NEWGRAMMARRULE
        End Function

        Public Shared Function SUBJECT() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$SUBJECT$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("NP")
            SUBJECT = NEWGRAMMARRULE
        End Function

        ''' <SUMMARY>
        '''DECLARITIVE PREDICATES
        '''CLAUSE = SUBJECT (NP) PREDICATE = PRED(VP) (PUNCT = .) 					
        '''CLAUSE = SUBJECT (NP) PREDICATE = PRED(VITV) (PP) (NP)					THE CAT WALKED) ON) THE ROAD>	(OBJECT) = THE ROAD / SUBJECT = THE CAT / (ACTION) = WALKED
        '''CLAUSE = SUBJECT (NP) PREDICATE = PRED(VTV)							THE CAT) WALKED>				(ACTION) = WALKED / (SUBJECT) = THE CAT
        '''CLAUSE = SUBJECT (NP) PREDICATE = PRED(V) COMPLMENT(NP) = VP			THE MAN WALKED) THE CAT>		(OBJECT) = CAT / (SUBJECT) THE MAN
        '''CLAUSE = SUBJECT (NP) PREDICATE = PRED(VAV) PRED(V) COMPLMENT(NP) = VP	THE MAN) IS WALKING) THE CAT>	(OBJECT) = CAT / (SUBJECT) THE MAN
        '''CLAUSE = SUBJECT (NP) PREDICATE = PRED(VAV) COMPLMENT(V) = VP			THE CAT) IS WALKING>			(ACTION) = WALKING / (SUBJECT) THE CAT
        ''' </SUMMARY>
        ''' <RETURNS></RETURNS>
        Public Shared Function PREDICATE() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$PREDICATE$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$VP$ $PUNCT$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$VITV$ $PP$ $NP$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$VITV$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$V$ $NP$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$VAV$ $V$ $NP$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$V$ $SUBJECT$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$VAV$ $V$ $SUBJECT$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$VITV$ $PP$ $SUBJECT$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$VITV$ $SUBJECT$")
            PREDICATE = NEWGRAMMARRULE
        End Function

        ''' <SUMMARY>
        '''EXCLAMITORY
        '''PURPOSE: TO EXPRESS STRONG EMOTIONS (FEELINGS)
        '''PUNCTUATION "!" ":)" EMOJI
        '''USED TO GIVE EMPHASIS
        '''DECLARATIVE SENTENCE
        '''EXCLAMITORY
        '''CLAUSE = SUBJECT (NP) PREDICATE = PRED(VP) (PUNCT = !)	CLAUSE> !
        ''' </SUMMARY>
        ''' <RETURNS></RETURNS>
        Public Shared Function EXCLAMITORY() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$EXCLAMITORY$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$SUBJECT$ $PREDICATE$ $!$")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$DECLARITIVE$ .")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$DECLARITIVE$.")
            EXCLAMITORY = NEWGRAMMARRULE
        End Function

        ''' <SUMMARY>
        '''INTEROGATIVE
        ''' QUESTWORD> CLAUSE> (PUNCT = ?)	WHERE/WHY/WHO/WHEN/WHAT> CLAUSE> ?>
        '''INTEROGATIVE
        '''PURPOSE:TO ASK A QUESTION / TO GET INFORMATION
        '''PUNCTUATION "?"
        '''INTERROGATIVE SENTENCES ARE USED TO FORM QUESTIONS.
        '''ONE FORM OF AN INTERROGATIVE SENTENCE IS A DECLARATIVE SENTENCE FOLLOWED BY A QUESTION MARK..
        '''INTERROGATIVE SENTENCE>= DECLARATIVE SENTENCE>"?"  = THE COMPUTER IS NOT WORKING? / AN ACTOR BECAME GOVERNOR?
        '''"WHO" PREDICATE>"?"   = WHO FIXED THE COMPUTER? / WHO WANTS TO DRINK WATER?
        '''("WHAT" |"WHICH") [ADVERB>* ADJECTIVE>] NOUN> PREDICATE>"?"   = WHICH FLOWER IS THE PRETTIEST? / WHAT BRIDGE GOES TO MANHATTAN?
        '''["WHAT" |"WHEN" |"WHERE" |"WHO" |"TO WHOM" |"WHY"] ("DO" |"DOES" |"DON'T" |"DOESN'T" |"DID" |"DIDN'T") SUBJECT>PREDICATE>"?"  = WHERE DOES JOHN LIVE?, DOES JOHN GO TO MANHATTAN?
        '''"WHICH"  [NOUN PHRASE>]("DO" |"DOES" |"DON'T" |"DOESN'T" |"DID" |"DIDN'T") SUBJECT> PREDICATE>"?" = WHICH FLOWER DO YOU LIKE BEST? / WHICH DIDN'T MARY TAKE HOME?
        '''["WHAT" |"WHICH" |"WHEN" |"WHERE" |"WHO" |"TO WHOM" |"WHY"] ("WILL" |"WON'T") SUBJECT> PREDICATE>"?" = WHAT WILL JOHN TAKE TO MANHATTAN? / WHEN WILL HE RETURN?
        '''["WHAT" |"WHICH" |"WHEN" |"WHERE" |"WHO" |"TO WHOM" | "WHY"] ("HAS" |"HAVE" |"HASN'T" |"HAVEN'T")SUBJECT>PREDICATE>"?"  = WILL JOHN BE THINKING ABOUT MARY? / WHY WILL JOHN CRY?
        '''"WHAT" |"WHICH" |"WHEN" |"WHERE" |"WHO" |"TO WHOM" | "WHY"] ("ARE" |"IS" |"WAS" |"WERE" |"AREN'T" |"ISN'T" |"WASN'T" |"WEREN'T")SUBJECT> [ADVERB>* ADJECTIVE> | PREP PHR>* | PREDICATE>]"?"  = WHY IS MARY COOKING NOODLES? / WHY HAVEN'T THE TULIPS FLOWERED?
        ''' </SUMMARY>
        ''' <RETURNS></RETURNS>
        Public Shared Function INTERROGATIVE() As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$INTERROGATIVE$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$DECLARITIVE$?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$DECLARITIVE$ ?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("WHO $PREDICATE$ ?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("WHO $PREDICATE$?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("WHAT $SUBJECT$ $PREDICATE$ ?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("WHAT $SUBJECT$ $PREDICATE$?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("WHAT $ADV$ $ADJ$ $N$ $PREDICATE$ ?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("WHAT $ADV$ $ADJ$ $N$ $PREDICATE$?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ $SUBJECT$ $PREDICATE$ ?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ $SUBJECT$ $PREDICATE$?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("WHICH $SUBJECT$ $PREDICATE$ ?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("WHICH $SUBJECT$ $PREDICATE$?")

            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ WILL $SUBJECT$ $PREDICATE$ ?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ WILL $SUBJECT$ $PREDICATE$?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ WONT $SUBJECT$ $PREDICATE$ ?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ WONT $SUBJECT$ $PREDICATE$?")

            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ HAS $SUBJECT$ $PREDICATE$ ?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ HAS $SUBJECT$ $PREDICATE$?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ HAVE $SUBJECT$ $PREDICATE$ ?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ HAVE $SUBJECT$ $PREDICATE$?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ WILL $SUBJECT$ $PREDICATE$ ?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ WILL $SUBJECT$ $PREDICATE$?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ HAVE NOT $SUBJECT$ $PREDICATE$ ?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ HAVE NOT $SUBJECT$ $PREDICATE$?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ HAS NOT $SUBJECT$ $PREDICATE$ ?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ HAS NOT $SUBJECT$ $PREDICATE$?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ WILL NOT $SUBJECT$ $PREDICATE$ ?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ WILL NOT $SUBJECT$ $PREDICATE$?")

            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ DO $SUBJECT$ $PREDICATE$ ?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ DO $SUBJECT$ $PREDICATE$?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ DOES $SUBJECT$ $PREDICATE$ ?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ DOES $SUBJECT$ $PREDICATE$?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ DID $SUBJECT$ $PREDICATE$ ?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ DID $SUBJECT$ $PREDICATE$?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ DID NOT $SUBJECT$ $PREDICATE$ ?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ DID NOT $SUBJECT$ $PREDICATE$?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ DOESNT NOT $SUBJECT$ $PREDICATE$ ?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ DIDNT $SUBJECT$ $PREDICATE$?")

            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ IS $SUBJECT$ $PREDICATE$ ?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ IS $SUBJECT$ $PREDICATE$?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ ARE $SUBJECT$ $PREDICATE$ ?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ ARE $SUBJECT$ $PREDICATE$?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ ISNT $SUBJECT$ $PREDICATE$ ?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ ISNT $SUBJECT$ $PREDICATE$?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ IS $SUBJECT$ NOT $PREDICATE$ ?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ IS $SUBJECT$ NOT $PREDICATE$?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ ARE $SUBJECT$ NOT $PREDICATE$ ?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ ARE $SUBJECT$ NOT $PREDICATE$?")

            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ $SUBJECT$ IS NOT $PREDICATE$ ?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ $SUBJECT$ IS NOT $PREDICATE$?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ $SUBJECT$ ARE $PREDICATE$ ?")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("$QUESTWORD$ $SUBJECT$ ARE NOT $PREDICATE$?")

            INTERROGATIVE = NEWGRAMMARRULE
        End Function

#End Region
#Region "SIMPLE CONCEPTNET GRAMMAR"



        ''' <SUMMARY>
        ''' USED TO DETECT TAG
        ''' USED TO GENERATE CLAUSES (EXPAND)
        ''' A POPULATED GRAMMAR
        '''     S → SUBJECT $A$ LINKINGVERB $LV$ $OBJECT$ $B$
        '''     S → SUBJECT $A$ CONCEPTPREDICATE $OBJECT$ $B$
        '''
        ''' SENTENCES
        ''' EXAMPLES OF SENTENCE PATTERNS (A|B) REPLACE WITH $SUBJECT$
        '''     $A$ $MADEOF$ $B$", "$S$")
        '''     $A$ $PARTOF$ $B$", "$S$")
        '''     $B$ $PROPERTYOF$ $A$", "$S$")
        '''     $B$ $FIRSTSUBEVENTOF$ $A$", "$S$")
        '''     $B$ $SUBEVENTOF$ $A$", "$S$")
        '''     $B$ $LASTSUBEVENTOF$ $A$", "$S$")
        '''     $B$ $EFFECTOF$ $A$", "$S$")
        '''     $B$ $DESIROUSEFFECTOF$ $A$", "$S$")
        '''     $B$ $DESIREOF$ $A$", "$S$")
        '''     $A$ $LOCATIONOF$ $B$", "$S$")
        '''     $A$ $MOTIVATIONOF$ $B$", "$S$")
        '''     $A$ $USEDFOR$ $B$", "$S$")
        '''     $A$ $ISA$ $B$", "$S$")
        '''     $A$ $DEFINEDAS$ $B$", "$S$")
        '''     $A$ $CAPEABLEOF$ $B$", "$S$")
        '''     $A$ $CAPABLEOFRECEIVINGACTION$ $B$", "$S$")
        ''' </SUMMARY>
        ''' <RETURNS></RETURNS>
        Public Shared Function SIMPLECONCEPTNETPHRASEGRAMMAR() As List(Of GrammarRule)
            'CONSTITUANTS

            Dim CUSTOMRULES As New List(Of GrammarRule)
            Dim NEWGRAMMARRULE As New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$DEFINEDAS$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS DEFINED AS")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("CAN BE EXPLAINED AS")
            CUSTOMRULES.Add(NEWGRAMMARRULE)
            NEWGRAMMARRULE = New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$ISA$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS A")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS AN")
            CUSTOMRULES.Add(NEWGRAMMARRULE)
            NEWGRAMMARRULE = New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$CAPEABLEOF$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS CAPEABLE OF")
            CUSTOMRULES.Add(NEWGRAMMARRULE)
            NEWGRAMMARRULE = New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$CAPABLEOFRECEIVINGACTION$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS CAPEABLE OF RECEIVING ACTION")
            CUSTOMRULES.Add(NEWGRAMMARRULE)
            NEWGRAMMARRULE = New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$DESIREOF$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS A DESIRE OF")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS THE DESIRE OF")
            CUSTOMRULES.Add(NEWGRAMMARRULE)
            NEWGRAMMARRULE = New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$DESIROUSEFFECTOF$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS A DESIROUS OUTCOME OF")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS A DESIROUS EFFECT OF")
            CUSTOMRULES.Add(NEWGRAMMARRULE)
            NEWGRAMMARRULE = New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$EFFECTOF$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS AN EFFECT OF")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS A EFFECT OF")
            CUSTOMRULES.Add(NEWGRAMMARRULE)
            NEWGRAMMARRULE = New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$FIRSTSUBEVENTOF$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS THE FIRST SUB-EVENT OF")
            CUSTOMRULES.Add(NEWGRAMMARRULE)
            NEWGRAMMARRULE = New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$SUBEVENTOF$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS A SUB-EVENT OF")
            CUSTOMRULES.Add(NEWGRAMMARRULE)
            NEWGRAMMARRULE = New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$LASTSUBEVENTOF$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS THE LAST SUB-EVENT OF")
            CUSTOMRULES.Add(NEWGRAMMARRULE)
            NEWGRAMMARRULE = New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$LOCATIONOF$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS LOCATED AT")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("ARE LOCATED AT")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("ARE LOCATED BY")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS LOCATED BY")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("ARE LOCATED IN")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS LOCATED IN")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("ARE LOCATED ON")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS LOCATED ON")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("ARE LOCATED BELOW")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS LOCATED BELOW")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("ARE LOCATED ABOVE")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS LOCATED ABOVE")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("ARE LOCATED NEXT TO")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS LOCATED NEXT TO")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("ARE LOCATED NEAR")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS LOCATED NEAR")
            CUSTOMRULES.Add(NEWGRAMMARRULE)
            NEWGRAMMARRULE = New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$MADEOF$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS MADE OF")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("ARE MADE OF")
            CUSTOMRULES.Add(NEWGRAMMARRULE)
            NEWGRAMMARRULE = New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$MOTIVATIONOF$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS THE MOTIVATION OF")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("ARE THE MOTIVATION OF")
            CUSTOMRULES.Add(NEWGRAMMARRULE)
            NEWGRAMMARRULE = New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$PARTOF$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS PART OF")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS A PART OF")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("ARE PART OF")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("ARE A PART OF")
            CUSTOMRULES.Add(NEWGRAMMARRULE)
            NEWGRAMMARRULE = New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$PREREQUISITEOF$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS THE PREREQUISITE OF")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("ARE THE PREREQUISITES OF")
            CUSTOMRULES.Add(NEWGRAMMARRULE)
            NEWGRAMMARRULE = New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$PROPERTYOF$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS THE PROPERTY OF")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("ARE THE PROPERTYS OF")
            CUSTOMRULES.Add(NEWGRAMMARRULE)
            NEWGRAMMARRULE = New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$USEDFOR$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("IS USED FOR")
            NEWGRAMMARRULE.COMPONENTSTRINGS.Add("ARE USED FOR")
            CUSTOMRULES.Add(NEWGRAMMARRULE)
            'PHRASEGRAMMARS - FOR REDUCTION
            NEWGRAMMARRULE = New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$CONCEPT$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS = New List(Of String) From {
                "$USEDFOR$",
                "$MADEOF$",
                "$PROPERTYOF$",
                "$FIRSTSUBEVENTOF$",
                "$SUBEVENTOF$",
                "$LASTSUBEVENTOF$",
                "$EFFECTOF$",
                "$DESIROUSEFFECTOF$",
                "$DESIREOF$",
                "$LOCATIONOF$",
                "$MOTIVATIONOF$",
                "$ISA$",
                "$DEFINEDAS$",
                "$CAPEABLEOF$",
                "$CAPABLEOFRECEIVINGACTION$"
            }
            CUSTOMRULES.Add(NEWGRAMMARRULE)

            NEWGRAMMARRULE = New GrammarRule With {
                .COMPONENTSTRINGS = New List(Of String),
                .TAGSTRING = "$DECLARITIVE_CONCEPT$"
            }
            NEWGRAMMARRULE.COMPONENTSTRINGS = New List(Of String) From {
                "$NP$ $CONCEPT$ $NP$."
            }
            CUSTOMRULES.Add(NEWGRAMMARRULE)

            Return CUSTOMRULES
        End Function

#End Region
    End Class
End Namespace
