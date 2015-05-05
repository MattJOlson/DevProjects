using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Gtk;

namespace MaddenGraph
{

    class FormationParser
    {
        public FormationParser(string filePath)
        {
            file_ = new StreamReader(filePath);
            state_ = FormationState.TITLE;
        }

        ~FormationParser()
        {
            file_.Dispose();
        }

        public Formation Parse()
        {
            while (!file_.EndOfStream) {
                ParseLine();
            }
            file_.Close();

            return formation_;
        }

        private void ParseLine()
        {
            string line = file_.ReadLine();

            char[] whitespace = {' ', '\t', '\r', '\n'};
            string[] tokens = line.Split(whitespace, StringSplitOptions.RemoveEmptyEntries);

            if (IsEmptyOrComment(tokens)) { return; }

            if (IsStateChange(tokens[0])) {
                ChangeState(tokens[0]); // rest of line gets ignored
                return;
            }

            switch (state_) {
            case FormationState.TITLE:
                ParseFormationName(tokens);
                break;
            case FormationState.STRONG:
                ParseLineTokens(tokens, FormationState.STRONG);
                break;
            case FormationState.WEAK:
                ParseLineTokens(tokens, FormationState.WEAK);
                break;
            case FormationState.BACKFIELD:
                ParseBackfieldTokens(tokens);
                break;
            default:
                // can't happen
                break;
            }
        }

        bool IsEmptyOrComment (string[] tokens)
        {
            if (0 == tokens.Length) { return true; }
            // We removed empty entries, so we can count on tokens[0] having
            //  a prefix character
            if ('#' == tokens[0][0]) { return true; }

            // Nope, tokens[0] didn't lead off with a hash, there's something
            //  in there (maybe just noise)
            return false;
        }

        bool IsStateChange (string str)
        {
            switch (str) {
            case "STRONG":
            case "WEAK":
            case "BACK":
                return true;
            default:
                return false;
            }
        }

        void ChangeState (string str)
        {
            switch (str) {
            case "STRONG":
                state_ = FormationState.STRONG;
                break;
            case "WEAK":
                state_ = FormationState.WEAK;
                break;
            case "BACK":
                state_ = FormationState.BACKFIELD;
                break;
            default:
                // can't happen
                break;
            }
        }

        ReceiverTag GetAsReceiver (string str)
        {
            switch (str) {
            case "X":
                return ReceiverTag.X;
            case "C":
                return ReceiverTag.CIRCLE;
            case "T":
                return ReceiverTag.TRI;
            case "S":
                return ReceiverTag.SQUARE;
            case "R":
                return ReceiverTag.R1;
            default:
                // Uh oh, expected a receiver but got nuttin!
                // FIXME: Should error
                return ReceiverTag.NONE;
            }
        }

        int GetAsBackfieldOffset (string str)
        {
            // Coming in we expect this to be a string that matches this regex
            Regex offset = new Regex(@"([0-9]+)([sw])");
            Match match  = offset.Match(str);

            if(match.Groups[2].Value == "s") {
                return Convert.ToInt32(match.Groups[1].Value);
            } else { // should be "w"
                return -Convert.ToInt32(match.Groups[1].Value);
            }
        }

        void ParseFormationName (string[] tokens)
        {
            string formName = string.Join(" ", tokens);
            formation_ = new Formation(formName);
        }

        void ParseLineTokens (string[] tokens, FormationState state)
        {
            string pos = tokens[0];
            bool onLine = "on" == tokens[1]; // XXX: case-sensitivity issue
            int split = 1;
            ReceiverTag rec = ReceiverTag.NONE;
            if (2 < tokens.Length) { // FIXME: Gotta be a better way to do this
                if (ReceiverTag.NONE == (rec = GetAsReceiver(tokens[2]))) {
                    // Didn't get a receiver out of the third token, so it's a split
                    //  and there might be a fourth
                    split = Convert.ToInt32(tokens[2]);
                    if (3 < tokens.Length) {
                        rec = GetAsReceiver(tokens[3]);
                    }
                } // else we've already assigned to rec
            }

            formation_.AddLinePlayer(pos, state_ == FormationState.WEAK, onLine, split, rec);
        }

        void ParseBackfieldTokens (string[] tokens)
        {
            string pos = tokens[0];
            int depth = Convert.ToInt32(tokens[1]); // depth is required
            int offset = 0;
            ReceiverTag rec = ReceiverTag.NONE;
            if(2 < tokens.Length) {
                if (ReceiverTag.NONE == (rec = GetAsReceiver(tokens[2]))) {
                    offset = GetAsBackfieldOffset(tokens[2]);
                    if(3 < tokens.Length) {
                        rec = GetAsReceiver(tokens[3]);
                    }
                } // else we've already assigned to rec
            }

            formation_.AddBackfieldPlayer(pos, depth, offset, rec);
        }

        enum FormationState { TITLE, STRONG, WEAK, BACKFIELD };
        private FormationState state_;
        private Formation formation_;
        private StreamReader file_;
    }
}
