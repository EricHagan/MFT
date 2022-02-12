![Alt text](MFT/img/Header_img.png?raw=true "Retrorefletive MFT assembly")
# FadeMate

## Introduction

<p>Microfade testing is a technique used in heritage conservation to determine if an object is highly light-sensitive, and estimate the rate of colour change with light dose.
Test results assist in the selection of display conditions (<em>i.e.</em> illuminance and display duration) that balance the conflicting needs for access and preservation.</p>
<p>The analytical technique typically requires multiple software applications to control peripheral components during lightfastness tests, and then process results. 
The aim of this project is to interface these components within a single open-source software package, and optimize the workflow. 
As a starting point, our focus is GUI development, and communication with a spectrometer to facilitate vis-reflectance calibration and data collection.</p>

## Technologies
<p>.NET/C#</p>
<p><a href="https://www.broadcom.com/products/optical-sensors/spectrometers/spectrometers-qmini/afbr-s20m2wu">Broadcom Spectrometer Software Development Kit (SDK)</a></p>

## Scope of functionalities

The following list outlines broad functionalities to address in this project:
<ul>
  <li>Create an interface for implementing different brands of spectrometer: communicate settings, measure spectra and plot results. (✓)</li>
          <ul>
            <li>Test with a Broadcom Qmini spectrometer and associated SDK</li>
        </ul>
  <li>Add controls for calibrating the reflectance spectrum: dark and white reference collection, and conversion of sample spectra to % reflectance.</li>
  <li>Implement spectrum smoothing, and resampling functions.</li>
  <li>Implement colorimetry functions for calculating colour values and colour difference: spectrum⤍CIEXYZ⤍CIELAB; ΔE.</li>
  <li>Perform an MFT experiment with data logged at user-defined time intervals, and stopped at a specified colour difference or time limit.</li>
  <li>Save/export data.</li>
</ul>

Adding these functionalities would be a bonus:
<ul>
  <li>Create an interface for controlling a light source, or a shutter/filter wheel using spectrometer I/O pins.</li>
  <li>Embed USB camera video feed into the application.</li>
</ul>

## Project status
<p>This project is currently in the initial phase of development. Primary areas of focus are drafting the GUI layout, interfacing with a spectrometer, and programming colourimetry functions.</p>

## Sources
<p>This project was inspired by the original MFT design by Paul Whitmore, and the development of alternate configurationas such as the retroreflecive assembly. 
It was also influenced by the Getty Spectral Viewer software package for post-processing vis-reflectance spectra logged during MFT experiments. </p>

### References
<ol>
<li>Whitmore, P. M., Pan, X., & Bailie, C. (1999). Predicting the fading of objects: Identification of fugitive colorants through direct nondestructive lightfastness measurements. 
<em>Journal of the American Institute for Conservation</em>, 38(3), 395-409.</li>
<li>Beltran, V. L. (2019). <a href="https://www.getty.edu/conservation/publications_resources/pdf_publications/pdf/Advancing_MFT_practice.pdf">Advancing microfading tester practice</a>. Los Angeles: Getty Conservation Institute.</li>
<li>Ford, Bruce. <a href="https://www.microfading.com/">microfading.com</a></li>
</ol>

## License
[![CC BY-NC-SA 4.0][cc-by-nc-sa-shield]][cc-by-nc-sa]

This work is licensed under a
[Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License][cc-by-nc-sa].

[![CC BY-NC-SA 4.0][cc-by-nc-sa-image]][cc-by-nc-sa]

[cc-by-nc-sa]: http://creativecommons.org/licenses/by-nc-sa/4.0/
[cc-by-nc-sa-image]: https://licensebuttons.net/l/by-nc-sa/4.0/88x31.png
[cc-by-nc-sa-shield]: https://img.shields.io/badge/License-CC%20BY--NC--SA%204.0-lightgrey.svg
