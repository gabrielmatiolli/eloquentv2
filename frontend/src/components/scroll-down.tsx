import { ChevronDown } from 'lucide-react'
import React from 'react'

export default function ScrollDown() {
  return (
    <ChevronDown className='text-foreground absolute bottom-2 left-1/2 transform -translate-x-1/2 animate-pulse' size={30} />
  )
}
