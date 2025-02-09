name: Bug Report
description: Submit a bug
title: "Bug: <Title>"
labels: ["Type: bug"]
body:
- type: textarea
  attributes:
    label: Bug Description
    description: Describe the bug that had occurred
    placeholder: |
      Please be as concise and descriptive as possible.
  validations:
    required: true
- type: textarea
  attributes:
    label: Reproduction Steps
    description: Steps to reproduce the Bug
    placeholder: |
      1.
      2.
      3.
  validations:
    required: true
- type: dropdown
  attributes:
    label: Platform
    description: What platform are you on?
    options:
		- Windows
		- Mac
		- Both
  validations:
    required: true
- type: dropdown
  attributes:
    label: Desktop or Laptop?
    description: Were you on a Desktop or Laptop when this happened?
    options:
		- Desktop
		- Laptop
		- Happens on Both
  validations:
    required: true
- type: dropdown
  attributes:
    label: (If Laptop) was your laptop plugged in?
    description: Was the laptop plugged in when the bug occurred?
    options:
		- Yes
		- No
  validations:
    required: false
- type: dropdown
  attributes:
    label: Editor or Build?
    description: Did this bug occur in the editor or in a build of the game?
    options:
		- Editor
		- Build
		- Both
  validations:
    required: true
- type: input
  attributes:
    label: (If Build) What Build?
    description: What build did this occur in? 
  validations:
    required: false
- type: input
  attributes:
    label: (If Editor) What Branch?
    description: What branch did this occur in? 
  validations:
    required: false
- type: input
  attributes:
    label: (If Editor) Most Recent Push
    description: What was the most recent push when the bug occurred? 
  validations:
    required: false
- type: dropdown
  attributes:
    label: (If Editor) Menu or Direct Load?
    description: When the bug occurred, did you load into the scene from the menu or did you load directly into the scene from the editor?
    options:
		- Menu
		- Editor
		- Both
  validations:
    required: false
- type: dropdown
  attributes:
    label: Every Time or First Load?
    description: Did the bug occur every time or only on the first time loading into the game/scene?
    options:
		- Every Time
		- First Load only
  validations:
    required: true
- type: dropdown
  attributes:
    label: Was the computer under heavy load?
    description: When the bug occurred, did you have RAM/CPU/GPU intensive tasks/programs running such as a browser with lots of tabs open, photoshop, etc?
    options:
		- Computer was under heavy load
		- Computer was NOT under heavy load
  validations:
    required: false
- type: textarea
  attributes:
    label: Anything else?
    description: Use this section to describe anything else that you feel is important to note.
  validations:
    required: false
