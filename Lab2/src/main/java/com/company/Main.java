package com.company;

import com.company.parsers.DOMParser;
import com.company.parsers.HTMLConverter;
import com.company.parsers.SAXParser;
import com.company.parsers.StAXParser;

public final class Main {

    public static void main(final String[] args) {

        //варіант 16

        DOMParser domParser = new DOMParser("input.xml");
        domParser.parseXML(true);
        domParser.sortXML();
        domParser.saveToXML();

        SAXParser saxParser = new SAXParser("input.xml");
        saxParser.parse(true);
        saxParser.sortXML();
        saxParser.writeXML();

        StAXParser stAXParser = new StAXParser("input.xml");
        stAXParser.parse();
        stAXParser.sortXML();
        stAXParser.writeXML();

        HTMLConverter htmlConverter = new HTMLConverter();
        htmlConverter.convertToHtml("input.xsl", "input.xml", "output.html");
    }
}
