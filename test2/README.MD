## CSharp
# 第二次作业

     
- 本次作业的目标
     
     1.使用Open XML SDK方法解析Word文件，提取出Word文件中的所有文字。文字按段落分组。使用WordprocessingDocument类作
     为实验项目的主要类。
     
     2.把方法封装到自定义类中。
     
- 在VS中通过管理NuGet程序包添加DocumentFormat.OpenXml，使用OpenXml可以对Word文件进行处理， 可以将Word中的所有文字解析并显示出来。
   
   简单的WordXML内容，它包括这几部分：
   
   XML的声明和名称空间的指明：
    <?xml version="1.0"?>
   <w:wordDocument xmlns:w="http://schemas.microsoft.com/office/word/2003/wordml">
   
   文档内容
   
   <<w:body>>…</w:body>
   
 - 从body内可以看出，构成实际文本内容的有3种类型节点：
   <<w:p>> 表示一个段落
   
   <<w:r>> 表示一个样式串，指明它包括的文本的显示样式
   
   <<w:t>> 表示真正的文本内容
   
   对齐方向：<w:jc w:val=”right”/> 这儿是右对齐。
   
   - 文档设置
     </w:body>
     
     <w:docPr>
     <w:view w:val="print"/><w:zoom w:percent="100"/>
     < /w:docPr>
     
     </w:wordDocument>
     
     docPr，就是document property的意思了。
     
     表示文档的视图是“print”，视图比例100%