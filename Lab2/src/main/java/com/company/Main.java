package com.company;

import com.company.parsers.DOMParser;
import com.company.parsers.HTMLConverter;
import com.company.parsers.SAXParser;
import com.company.parsers.StAXParser;

public final class Main {

    public static void main(final String[] args) {

        //варіант 16

        DOMParser domParser = new DOMParser(Consts.XML_FILE);
        domParser.parseXML(true);
        domParser.sortXML();
        domParser.saveToXML();

        SAXParser saxParser = new SAXParser(Consts.XML_FILE);
        saxParser.parse(true);
        saxParser.sortXML();
        saxParser.writeXML();

        StAXParser stAXParser = new StAXParser(Consts.XML_FILE);
        stAXParser.parse();
        stAXParser.sortXML();
        stAXParser.writeXML();

        HTMLConverter htmlConverter = new HTMLConverter();
        htmlConverter.convertToHtml(Consts.XSL_FILE, Consts.XML_FILE, Consts.HTML_RESULT);
    }
}
