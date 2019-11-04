#! /usr/bin/python

import sys
import os
import argparse
import requests

def get(train_datas, base_uri, branch, output_dir):
    for data in train_datas:
        file =  data + ".traineddata"
        uri = base_uri + "/raw/" + branch + "/" + file
        
        print(uri)
        response = requests.get(uri)
        print(response)
        
        if not response.ok:
            return False
        
        open(os.path.join(output_dir, file), 'wb').write(response.content)
         
    return True

best_repo = "https://github.com/tesseract-ocr/tessdata_best"
fast_repo = "https://github.com/tesseract-ocr/tessdata_fast"
default_repo = "https://github.com/tesseract-ocr/tessdata"

available_data = set([
    "afr", "amh", "ara", "asm", "aze", "aze_cyrl", "bel",
    "ben", "bod", "bos", "bre", "bul", "cat", "ceb", "ces",
    "chi_sim", "chi_sim_vert", "chi_tra", "chi_tra_vert",
    "chr", "cos", "cym", "dan", "deu", "div", "dzo", "ell",
    "eng", "enm", "epo", "est", "eus", "fao", "fas", "fil",
    "fin", "fra", "frk", "frm", "fry", "gla", "gle", "glg",
    "grc", "guj", "hat", "heb", "hin", "hrv", "hun", "hye",
    "iku", "ind", "isl", "ita", "ita_old", "jav", "jpn",
    "jpn_vert", "kan", "kat", "kat_old", "kaz", "khm",
    "kir", "kmr", "kor", "kor_vert", "lao", "lat", "lav",
    "lit", "ltz", "mal", "mar", "mkd", "mlt", "mon", "mri",
    "msa", "mya", "nep", "nld", "nor", "oci", "ori", "osd",
    "pan", "pol", "por", "pus", "que", "ron", "rus", "san",
    "sin", "slk", "slv", "snd", "spa", "spa_old", "sqi",
    "srp", "srp_latn", "sun", "swa", "swe", "syr", "tam",
    "tat", "tel", "tgk", "tha", "tir", "ton", "tur", "uig",
    "ukr", "urd", "uzb", "uzb_cyrl", "vie", "yid", "yor"])

default_data = set([
    "eng", "fra", "deu", "ita",
    "jpn", "por", "spa",
    "chi_sim", "chi_tra"])

specials_data =set(["osd", "equ"])


def main(argv=sys.argv[1:]):

    parser = argparse.ArgumentParser(prog="tessdata", description='Get training datas from tesseract')
    parser.add_argument('-b', '--best', action="store_true", help="For people willing to trade a lot of speed for slightly better accuracy.")
    parser.add_argument('-f', '--fast', action="store_true", help="These are a speed/accuracy compromise as to what offered the best value for money in speed vs accuracy.")
    parser.add_argument('--all', action="store_true", help="Get all training datas")
    parser.add_argument('--dir', type=str, help="tessadata directory")
    
    args = parser.parse_args(argv)

    if not args.dir:
        output = "."
    else:
        output = args.dir

    repo = default_repo
    if args.best:
        repo = best_repo
    if args.fast:
        repo = fast_repo
    
    data = default_data
    if args.all:
        data = available_data

    if not get(data, repo, "master", output):
        print("Failed!")
        exit(-1)
        
    if not get(specials_data, default_repo, "3.04.00", output):
        print("Failed!")
        exit(-1)
    
    print("Succeed!")
    exit(0)
   
     
if __name__ == "__main__":
    main()
