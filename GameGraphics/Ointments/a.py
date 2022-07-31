import shutil
import os

files = os.listdir()
for file in files:
    if(file == "a.py" or file == "res"):
        continue
        
    
    shutil.copyfile(file,"res/"+file.upper())